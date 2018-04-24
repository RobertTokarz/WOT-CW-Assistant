using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOT_CW_Assistant.Models;

namespace WOT_CW_Assistant.Controllers
{
    public abstract partial class BaseController : Controller
    {
        private ApplicationDbContext context;
        protected virtual void UpdatePlayersInClan(string playerNickName)
        {
            try
            {
                string url = "https://api.worldoftanks.eu/wot/account/list/?application_id=9d3d88ea7bec100a6a1c71edc0e12416&tier&search=" + playerNickName + "&type=exact";
                using (var webClient = new System.Net.WebClient())
                {
                    context = new ApplicationDbContext();
                    string playerJson = webClient.DownloadString(url);
                    JObject playerJObject = JObject.Parse(playerJson);
                    string playerId = playerJObject["data"][0]["account_id"].ToString();
                    string detailsUrl = "https://api.worldoftanks.eu/wot/account/info/?application_id=9d3d88ea7bec100a6a1c71edc0e12416&account_id=" + playerId;
                    string playerDetailsJson = webClient.DownloadString(detailsUrl);
                    JObject playerDetails = JObject.Parse(playerDetailsJson);
                    string clanId = playerDetails["data"][playerId]["clan_id"].ToString();

                    var dbPlayers = context.Players.Where(p => p.clanId == clanId).ToList();
                    List<Player> playerList = GetClanMembers(clanId);
                    List<Player> playersNotInClan = dbPlayers.Where(e1 => !playerList.Any(e2 => e2.playerNickName == e1.playerNickName)).ToList(); //players to delete - who quit from clan
                    List<Player> playersToAddToClan = playerList.Where(e1 => !dbPlayers.Any(e2 => e2.playerNickName == e1.playerNickName)).ToList(); //players to add - new players in clan
                    context.Players.RemoveRange(playersNotInClan);
                    context.Players.AddRange(playersToAddToClan);
                    context.SaveChanges();
                    context.Dispose();
                    if (playersToAddToClan.Count() > 0) { AddTanksStats(playersToAddToClan); }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        protected virtual void AddTanksStats(List<Player> clanPlayers) //add update tanks stats
        {
            try
            {
                context = new ApplicationDbContext();
                List<Tank> tList = context.Tanks.ToList();
                List<string> tanksList = context.Tanks.ToList().Select(t => t.tankNo).ToList();
                foreach (Player newPlayer in clanPlayers)
                {
                    int queriedTanks = 0;
                    List<TankStatistics> newTanksStatisticList = new List<TankStatistics>();
                    List<TankStatistics> updateTanksStatisticList = new List<TankStatistics>();
                    int listLength = 100;
                    while (tanksList.Count() > queriedTanks)
                    {

                        try
                        {
                            List<string> tanksSublist = tanksList.GetRange(queriedTanks, listLength); //selecting max of 100 tanks per query
                            string tanks = string.Join(",", tanksSublist);

                            using (var webClient = new System.Net.WebClient())
                            {
                                string statsUrl = "https://api.worldoftanks.eu/wot/tanks/stats/?application_id=9d3d88ea7bec100a6a1c71edc0e12416&account_id=" + newPlayer.playerNo + "&tank_id=" + tanks;
                                string playerStats = webClient.DownloadString(statsUrl);
                                JObject playerStatsJObj = JObject.Parse(playerStats);
                                JArray playerStatsJArray = playerStatsJObj["data"][newPlayer.playerNo] as JArray;
                                if (playerStatsJArray != null)
                                {

                                    foreach (JObject tankStatsObj in playerStatsJArray)
                                    {
                                        TankStatistics tankStatisctic = new TankStatistics();
                                        tankStatisctic.tankId = tankStatsObj["tank_id"].ToString();
                                        tankStatisctic.tank = tList.Where(t => t.tankNo == tankStatisctic.tankId).Select(t => t.tankName).FirstOrDefault();
                                        tankStatisctic.playerNo = newPlayer.playerNo;
                                        tankStatisctic.lastUpdate = DateTime.Now;
                                        tankStatisctic.damageDealt = (int)tankStatsObj["all"]["damage_dealt"];
                                        tankStatisctic.battlesCount = (int)tankStatsObj["all"]["battles"];
                                        tankStatisctic.avgExperiencePerBattle = (int)tankStatsObj["all"]["battle_avg_xp"];
                                        tankStatisctic.spotted = (int)tankStatsObj["all"]["spotted"];
                                        tankStatisctic.avgDamageBlocked = (int)tankStatsObj["all"]["avg_damage_blocked"];
                                        int battlesWon = (int)tankStatsObj["all"]["wins"];
                                        double winningPercent = 0;
                                        tankStatisctic.playerNickName = newPlayer.playerNickName;
                                        if (tankStatisctic.battlesCount != 0)
                                        {
                                            tankStatisctic.avgDamagePerBattle = (int)((double)(tankStatisctic.damageDealt / tankStatisctic.battlesCount));
                                            tankStatisctic.spotPerBattle = Math.Round((double)tankStatisctic.spotted / tankStatisctic.battlesCount, 2);
                                            winningPercent = (double)battlesWon / tankStatisctic.battlesCount * 100;
                                        }
                                        else
                                        {
                                            tankStatisctic.avgDamagePerBattle = 0;
                                            tankStatisctic.spotPerBattle = 0;
                                        }
                                        tankStatisctic.winningPercent = Convert.ToInt32(winningPercent);
                                        TankStatistics dbTankPlayerStats = context.TankStatistics.Where(p => p.playerNo == newPlayer.playerNo && p.tankId == tankStatisctic.tankId).FirstOrDefault(); //check if player stats are in table - for exaple player was already in different clan
                                        if (dbTankPlayerStats == null)   //new player or tank without stats in db - if player has stats update below
                                        {

                                            context.TankStatistics.Add(tankStatisctic);
                                            context.SaveChanges();
                                        }
                                        else  //update stats for players who have stats in db already
                                        {
                                            tankStatisctic.id = dbTankPlayerStats.id;
                                            context.Entry(dbTankPlayerStats).CurrentValues.SetValues(tankStatisctic);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                                queriedTanks += 100;
                                if (queriedTanks > tanksList.Count() - 100)   //only 100 responses per page
                                {
                                    listLength = tanksList.Count() - queriedTanks;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        protected virtual List<Player> GetClanMembers(string clanId)
        {
            List<Player> playerList = new List<Player>();
            using (var webClient = new System.Net.WebClient())
            {
                string clanMemberRequest = "https://api.worldoftanks.eu/wgn/clans/info/?application_id=9d3d88ea7bec100a6a1c71edc0e12416&clan_id=" + clanId;
                string jsonClanMembers = webClient.DownloadString(clanMemberRequest);
                JObject jsonClanMembersObj = JObject.Parse(jsonClanMembers);
                JToken membersData = jsonClanMembersObj.SelectToken("data." + clanId + ".members");
                JArray membersDataArray = (JArray)membersData;
                foreach (var member in membersDataArray)
                {
                    Player player = new Player();
                    player.playerNo = member.SelectToken("account_id", false).ToString();
                    player.playerNickName = member.SelectToken("account_name", false).ToString();
                    player.role = member.SelectToken("role", false).ToString();
                    player.clanId = clanId;
                    string playerStatsRequest = "https://api.worldoftanks.eu/wot/account/info/?application_id=9d3d88ea7bec100a6a1c71edc0e12416&account_id=" + player.playerNo;
                    string jsonPlayerStats = webClient.DownloadString(playerStatsRequest);
                    JObject jsonPlayerStatsObj = JObject.Parse(jsonPlayerStats);
                    player.personalRating = Int32.Parse(jsonPlayerStatsObj.SelectToken("data." + player.playerNo + ".global_rating", false).ToString());
                    player.avgExperience = Int32.Parse(jsonPlayerStatsObj.SelectToken("data." + player.playerNo + ".statistics.all.battle_avg_xp", false).ToString());
                    player.battles = Int32.Parse(jsonPlayerStatsObj.SelectToken("data." + player.playerNo + ".statistics.all.battles", false).ToString());
                    player.hitPercent = Int32.Parse(jsonPlayerStatsObj.SelectToken("data." + player.playerNo + ".statistics.all.hits_percents", false).ToString());

                    playerList.Add(player);
                }
            }
            return playerList;
        }
    }
}
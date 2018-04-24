using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOT_CW_Assistant.Models;
using WOT_CW_Assistant.Extensions;

namespace WOT_CW_Assistant.Controllers
{
    public class TankStatisticController : Controller
    {
         
        private ApplicationDbContext context;
        public TankStatisticController()
        {
            context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        [HttpPost]
        public ActionResult Index(List<WOT_CW_Assistant.Models.Tank> tanks)
        {
            
            List<TankStatistics> ordered = new List<TankStatistics>();
            try
            {
                List<TankStatistics> tsl = new List<TankStatistics>();
                string plNick = User.Identity.GetPlayerNickName();
                Player loggedPlayer = context.Players.Where(p => p.playerNickName == plNick).FirstOrDefault();
                string clanId = loggedPlayer.clanId;
                var dbPlayers = context.Players.Where(p => p.clanId == loggedPlayer.clanId).ToList();

                foreach (Player player in dbPlayers)
                {
                    foreach (Tank tank in tanks)
                    {
                        if (tank.selected)
                        {
                            TankStatistics ts = context.TankStatistics.Where(t => t.playerNo == player.playerNo && t.tankId == tank.tankNo).FirstOrDefault();
                            if (ts != null) { tsl.Add(ts); }
                        }
                    }
                }
                ordered = tsl.OrderByDescending(x => x.avgExperiencePerBattle).ToList();
                HttpContext.Session.Add("tanksStatistics", tsl);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View(ordered);
        }
        
        public ActionResult OrderBy(string orderBy)
        {

            List<TankStatistics> tsl = HttpContext.Session["tanksStatistics"] as List<TankStatistics>;
            List<TankStatistics> ordered = new List<TankStatistics>();
            switch (orderBy)
            {
                case "avgExp":
                    ordered = tsl.OrderByDescending(x => x.avgExperiencePerBattle).ToList();
                    break;
                case "battlesCount":
                    ordered = tsl.OrderByDescending(x => x.battlesCount).ToList();
                    break;
                case "nickName":
                    ordered = tsl.OrderBy(x => x.playerNickName).ToList();
                    break;
                case "tank":
                    ordered = tsl.OrderByDescending(x => x.tank).ToList();
                    break;
                case "avgDmg":
                    ordered = tsl.OrderByDescending(x => x.avgDamagePerBattle).ToList();
                    break;
                case "avgSpot":
                    ordered = tsl.OrderByDescending(x => x.spotPerBattle).ToList();
                    break;
                case "winPercent":
                    ordered = tsl.OrderByDescending(x => x.winningPercent).ToList();
                    break;
           }

            return View("Index", ordered);
        }

 

    }
}
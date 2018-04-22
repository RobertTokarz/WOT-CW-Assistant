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
        public ActionResult Index(List<WOT_CW_Assistant.Models.Tank> tanks, string orderBy = "avgExperiencePerBattle")
        {
            string plNick = User.Identity.GetPlayerNickName();
            Player loggedPlayer = context.Players.Where(p => p.playerNickName == plNick ).FirstOrDefault();
            string clanId = loggedPlayer.clanId;
            var dbPlayers = context.Players.Where(p => p.clanId == loggedPlayer.clanId).ToList();
            List<TankStatistics> tsl = new List<TankStatistics>();
            foreach(Player player in dbPlayers)
            {
                foreach(Tank tank in tanks)
                {
                    if (tank.selected)
                    {
                        TankStatistics ts = context.TankStatistics.Where(t => t.playerNo == player.playerNo && t.tankId == tank.tankNo).FirstOrDefault();
                        if (ts != null) { tsl.Add(ts); }
                    }

                }

            }

            List<TankStatistics> ordered = tsl.OrderByDescending(x=> x.avgExperiencePerBattle).ToList();
            return View(ordered);
        }



    }
}
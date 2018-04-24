using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WOT_CW_Assistant.Extensions;
using WOT_CW_Assistant.Models;

namespace WOT_CW_Assistant.Controllers
{
    public class PlayersController : BaseController
    {
        private ApplicationDbContext context;
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Players
        public ActionResult Index(bool updateTanksStats = false, bool updatePlayerStats = false)
        {
            context = new ApplicationDbContext();
            string playerNickName = User.Identity.GetPlayerNickName();
            string clanId = context.Players.Where(p => p.playerNickName == playerNickName).FirstOrDefault().clanId;
            List<Player> members = context.Players.Where(p => p.clanId == clanId).ToList();
            
            if (updateTanksStats)
            {
                AddTanksStats(members);
            }
            if(updatePlayerStats)
            {
                List<Player> updatedMembers = GetClanMembers(clanId);
                foreach(Player member in members)
                {
                    Player updated = updatedMembers.Where(p => p.playerNo == member.playerNo).FirstOrDefault();
                    updated.id = member.id;
                    context.Entry(member).CurrentValues.SetValues(updated);
                    context.SaveChanges();
                }
                members = updatedMembers;
            }
            context.Dispose();
            return View(members);
        }
        [HttpPost]
        public ActionResult UpdatePlayersStats(List<Player> members)
        {
            Task.Factory.StartNew(() => { AddTanksStats(members); }); //update stats
            return View("Index", members);
        }

    }
}
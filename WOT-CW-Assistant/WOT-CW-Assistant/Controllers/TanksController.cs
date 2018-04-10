using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOT_CW_Assistant.Models;

namespace WOT_CW_Assistant.Controllers
{
    public class TanksController : Controller
    {
        private ApplicationDbContext context;
        public TanksController()
        {
            context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        // GET: Tanks
        public ActionResult Index(string tier)
        {

            //var model = context.Tanks.Where(t => t.tankTier == Tier).ToList();
            //string url = "https://api.worldoftanks.eu/wot/encyclopedia/vehicles/?application_id=9d3d88ea7bec100a6a1c71edc0e12416&tier=" + Tier;
            //using (var webClient = new System.Net.WebClient())
            //{

            //    string json = webClient.DownloadString(url);

            //    JObject clan = JObject.Parse(json);
            //    JToken tanksData = clan.SelectToken("data");
            //    foreach(JToken tank in tanksData.Children())
            //    {
            //        JToken tankData = tank.First;
            //        string tankNo = tankData["tank_id"].ToString();
            //        string tankName = tankData["name"].ToString();
            //        string tankType = tankData["type"].ToString();
            //        string tankImage = tankData["images"]["big_icon"].ToString();
            //        string nation = tankData["nation"].ToString();
            //        Tank tankEntity = new Tank() { tankNo = tankNo, tankName = tankName, tankTier = Tier, tankType = tankType, tankImage = tankImage , nation = nation};
            //        model.Add(tankEntity);

            //    }
            //    context.Tanks.AddRange(model);
            //    context.SaveChanges();
            //}
            //context.Dispose();

            ViewBag.Tier = tier;
            return View();
        }

        public ActionResult TanksByType(string tankType, string tier)
        {
            var model = context.Tanks.Where(t => t.tankType == tankType && t.tankTier == tier).ToList();

            return View(model);

        }
    }
}
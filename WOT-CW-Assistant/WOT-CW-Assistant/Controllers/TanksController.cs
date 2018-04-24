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
            ViewBag.Tier = tier;
            return View();
        }

        public ActionResult TanksByType(string tankType, string tier)
        {
            var model = context.Tanks.Where(t => t.tankType == tankType && t.tankTier == tier).ToList();

            return View(model);

        }

        public ActionResult AllTanks()
        {
            var model = context.Tanks.ToList();

            return View(model);

        }
    }
}
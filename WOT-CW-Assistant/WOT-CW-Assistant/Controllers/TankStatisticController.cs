using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOT_CW_Assistant.Controllers
{
    public class TankStatisticController : Controller
    {
        [HttpPost]
        public ActionResult Index(List<WOT_CW_Assistant.Models.Tank> tanks)
        {

            return View();
        }
    }
}
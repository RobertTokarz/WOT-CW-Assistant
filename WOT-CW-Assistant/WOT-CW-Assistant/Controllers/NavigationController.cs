using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WOT_CW_Assistant.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Menu()
        {
            return PartialView("_Navigation");
        }
    }
}
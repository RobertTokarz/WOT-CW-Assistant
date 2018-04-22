using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOT_CW_Assistant.Models;
using WOT_CW_Assistant.Extensions;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WOT_CW_Assistant.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AfterLogin()
        {
            string playerNickName = User.Identity.GetPlayerNickName();
            var t = Task.Factory.StartNew(() => { UpdatePlayersInClan(playerNickName); }); //it is time consuming so better to run in separate thread

            return View("index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
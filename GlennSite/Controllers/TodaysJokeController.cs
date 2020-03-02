using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlennSite.Controllers
{
    public class TodaysJokeController : Controller
    {
        // GET: TodaysJoke
        public ActionResult TodaysJoke()
        {
            return View();
        }
    }
}
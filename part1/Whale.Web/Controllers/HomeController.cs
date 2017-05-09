using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whale.Web.Models;

namespace Whale.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            var slogans = model.Slogans;
            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitSlogan(HomeViewModel viewModel)
        {
            return View();
        }
    }
}
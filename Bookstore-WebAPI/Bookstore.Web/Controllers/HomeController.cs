﻿using System.Web.Mvc;

namespace Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //public ActionResult Index()
        //{
        //    HomePageViewModel viewModel = this.homeService.GetHomePageViewModel();
        //    return View(viewModel);
        //}

        public ActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
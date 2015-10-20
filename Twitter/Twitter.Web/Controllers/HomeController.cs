﻿
namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Twitter.Data;
    using System.Web.Mvc.Expressions;
    using Twitter.Web.ViewModels; // https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers - ASP.NET.MVC 5.2.3

    public class HomeController : BaseController // must install Ninject.MVC5 (to use constructor with parametеrs)
    {
        public HomeController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            var allTweets = this.Data.AllTweets.All().AsQueryable()
                .OrderByDescending(t => t.Tweet.SentToDate)
                .ThenByDescending(t => t.Id)
                .Select(TweetViewModel.Create);
            var startPage = RouteData.Values["id"] ?? 0;
            allTweets = allTweets.Skip((int)startPage).Take(9);

            if (allTweets == null)
            {
                return this.HttpNotFound("non existing tweets");
            }

            return this.View(allTweets);
        }

        public ActionResult About()
        {

            return this.RedirectToAction(x => x.Index());
        }
    }
}

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
            allTweets = allTweets.Skip((int)startPage).Take(6);

            if (allTweets == null)
            {
                return this.HttpNotFound("non existing tweets");
            }

            return this.View(allTweets);
        }

       /* public ActionResult Index(int page = 1)
        {
            const int PageSize = 1;
            var allTweets = this.Data.AllTweets.All()
                .OrderByDescending(t => t.Tweet.SentToDate)
                .ThenByDescending(t => t.Id)
                .Select(TweetViewModel.Create);
            var tweetsCount = allTweets.Count();
            var pageCount = (tweetsCount + PageSize - 1) / PageSize;

            ViewData["page"] = page;
            ViewData["count"] = pageCount;

            return this.View(this.Data.AllTweets.All().Skip((page - 1) * PageSize).Take(PageSize));
        }*/

       /* public ActionResult About()
        {

            return this.RedirectToAction(x => x.Index());
        }*/
    }
}

namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Twitter.Data;
    using Twitter.Web.ViewModels;
    using Twitter.Models;
    using System;

    [Authorize]
    public class CategoriesController : BaseController
    {
        public CategoriesController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            var categories = this.Data.Categories.All()
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Id)
                .Select(CategoryViewModel.Create);

            if (categories == null)
            {
                return this.HttpNotFound("non existing categories");
            }

            return this.View(categories);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddTweetInCategory(Tweet tweet, int id)
        {
            if (ModelState.IsValid)
            {
                this.Data.Tweets.Add(new Tweet
                {
                    Title = tweet.Title,
                    Details = tweet.Details,
                    ImageUrl = tweet.ImageUrl,
                    SentToDate = DateTime.Now,
                    CategoryId = id,
                    AuthorId = this.UserProfile.Id
                });
                this.Data.SaveChanges();
                //return RedirectToAction("NewTweet");
            }

            return this.View();
        }

        public ActionResult TweetsByCategory(string name)
        {
            var tweetsByCategory = this.Data.AllTweets.All()
                .Include(t => t.Tweet)
                .Include("Tweet.Category")
                .Where(t => t.Tweet.Category.Name == name)
                .OrderByDescending(t => t.Tweet.SentToDate)
                .ThenByDescending(t => t.Tweet.Id)
                .Select(TweetViewModel.Create);
            var startPage = RouteData.Values["id"] ?? 0;
            tweetsByCategory = tweetsByCategory.Skip((int)startPage).Take(9);

            if (tweetsByCategory == null)
            {
                return this.Content("non existing tweets in this categories");
            }

            return this.View(tweetsByCategory);
        }

    }
}
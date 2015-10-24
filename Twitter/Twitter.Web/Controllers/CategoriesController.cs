
namespace Twitter.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Twitter.Data;
    using Twitter.Web.ViewModels;
    using Twitter.Models;

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

        public ActionResult NewTweet()
        {
            var thisTweet = this.Data.Tweets.All()
                    .Include(t => t.Category)
                    .Where(t => t.AuthorId == this.UserProfile.Id)
                    .OrderByDescending(t => t.SentToDate)
                    .Select(NewTweetViewModel.Create)
                    .FirstOrDefault();

            return this.View(thisTweet);
        }

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

                return this.RedirectToAction("NewTweet");
            }

            return this.View();
        }

        public ActionResult AddTweetInAllTweets(int id)
        {
            this.Data.AllTweets.Add(new UserTweet
                {
                    TweetId = id,
                    AuthorId = this.UserProfile.Id
                });
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult EditNewTweet(int id, Tweet newTweet)
        {
            var currentTweet = this.Data.Tweets.Find(id);
            if (currentTweet == null)
            {
                return this.HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                currentTweet.Title = newTweet.Title;
                currentTweet.Details = newTweet.Details;
                currentTweet.ImageUrl = newTweet.ImageUrl;
                this.Data.SaveChanges();
                return RedirectToAction("NewTweet");
            }

            return this.View(currentTweet);
        }

        public ActionResult DeleteNewTweet(int id)
        {
            var tweet = this.Data.Tweets.Find(id);
            if(tweet == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Tweets.Delete(tweet);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult TweetsByCategory(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
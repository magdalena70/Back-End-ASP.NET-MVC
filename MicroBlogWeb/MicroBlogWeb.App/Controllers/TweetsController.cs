using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MicroBlogWeb.Data;
using MicroBlogWeb.Models;
using Microsoft.AspNet.Identity;
using System;

namespace MicroBlogWeb.App.Controllers
{
    public class TweetsController : Controller
    {
        private MicroBlogWebContext db = new MicroBlogWebContext();

        // GET: Tweets
        [AllowAnonymous]
        public ActionResult Index()
        {
            var tweets = db.Tweets
                .OrderByDescending(t => t.TimeOfPosting)
                .ToList();

            return View(tweets);
        }

        // GET: Tweets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        // POST: Tweets/Details/5
        [Authorize]
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult AddInFavorites(int id)
        {
            Tweet tweet = db.Tweets.Find(id);
            User currentUser = db.Users.Find(User.Identity.GetUserId());
            if (tweet.Author.Id == currentUser.Id)
            {
                this.TempData["Error"] = "You can not take your oun tweet in Favorites.";
                return View(tweet);
            }

            currentUser.FavouriteTweets.Add(tweet);
            db.SaveChanges();

            return RedirectToAction("Profile/" + currentUser.Id, "Users");
        }

        // GET: Tweets/Retweet/5
        [Authorize]
        public ActionResult Retweet(int id)
        {
            return View();
        }

        // POST: Tweets/Retweet
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Retweet([Bind(Include = "Id,Content,ImageUrl")] Tweet tweet, int id)
        {
            Tweet currentTweet = db.Tweets.Find(id);

            tweet.Author = db.Users.Find(User.Identity.GetUserId());
            tweet.TimeOfPosting = DateTime.Now;
            currentTweet.Retweets.Add(tweet);
            db.SaveChanges();

            return RedirectToAction("Details/" + currentTweet.Id, "Tweets");
        }

        // GET: Tweets/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tweets/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,ImageUrl,TimeOfPosting")] Tweet tweet)
        {
            tweet.TimeOfPosting = DateTime.Now;
            tweet.Author = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.Tweets.Add(tweet);
                db.SaveChanges();
                return RedirectToAction("Index", "Tweets");
            }

            return View(tweet);
        }

        // GET: Tweets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }

            return View(tweet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,ImageUrl,TimeOfPosting")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tweet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + tweet.Id, "Tweets");
            }
            return View(tweet);
        }

        // GET: Tweets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        // POST: Tweets/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tweet tweet = db.Tweets.Find(id);
            db.Tweets.Remove(tweet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

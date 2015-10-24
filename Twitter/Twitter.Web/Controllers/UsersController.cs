
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

    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            :base(data)
        {
        }

        [Authorize]
        public ActionResult Index(string username)
        {
            var user = this.Data.Users.All()
                .Include(u => u.Tweets)
                .Include("Tweets.Tweet")
                .Include("Tweets.Tweet.Category")
                .Include("Tweets.Favorites")
                .Include("Favorites.UserTweets.Tweet")
                .Include("Favorites.UserTweets.User")
                .Include(u => u.Notifications)
                .Include("Notifications.Notification")
                .Include(u => u.Messages)
                .Include(u => u.Groups)
                .Where(u => u.UserName == username)
                .Select(UserViewModel.CreateUser)
                .FirstOrDefault();
            if (user == null)
            {
                return this.HttpNotFound("non existing user");
            }

            return this.View(user);
        }

        [Authorize]
        public ActionResult UserTweets(string username)
        {
            var user = this.Data.Users.All()
                .Include(u => u.Tweets)
                .Include("Tweets.Tweet")
                .Include("Tweets.Tweet.Category")
                .Include("Tweets.Favorites")
                .Include("Favorites.UserTweets.Tweet")
                .Include("Favorites.UserTweets.User")
                .Select(u => new UserViewModel
                {
                    UserName = u.UserName,
                    AvatarUrl = u.AvatarUrl != null ?
                        u.AvatarUrl : 
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJNGKxDl-q0wRp-eKqFc1jzuKeGA_tldmvO71crqFQ8ptsqIjk",
                    UserTweets = u.Tweets.AsQueryable().Select(UserTweetViewModel.CreateTweet)
                })
                .FirstOrDefault();
            if (user == null)
            {
                return this.HttpNotFound("non existing user");
            }

            return this.View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Likes(int id)
        {
            var hasExistingLike = this.Data.Favorites.All()
                .Any(f => f.FanId == this.UserProfile.Id && f.userTweetId == id);
            if (!hasExistingLike)
            {
                this.Data.Favorites.Add(new Favorite
                {
                    FanId = this.UserProfile.Id,
                    userTweetId = id
                });
                this.Data.SaveChanges();
            }

            var likes = this.Data.Favorites.All().Where(f => f.userTweetId == id);
            var likesCount = likes.Count();
            var fans = likes.Select(f => f.Fan.UserName).ToList();

            return this.Content(string.Format("{0} ({1})", likesCount, string.Join(", ", fans)));
        }

        [Authorize]
        public ActionResult UserFavoriteTweets(string username)
        {
            var favorites = this.Data.Favorites.All()
                .Include(f => f.UserTweet)
                .Include("UserTweet.Tweet")
                .Include("UserTweet.Tweet.Category")
                .Where(f => f.Fan.UserName == username)
                .OrderByDescending(f => f.UserTweet.Tweet.SentToDate)
                .ThenByDescending(f => f.UserTweet.Tweet.Id)
                .Select(FavoritesViewModel.Create);

            if (favorites == null)
            {
                return this.HttpNotFound("non existing favorite tweets");
            }

            return this.View(favorites);
        }

        public ActionResult AllUsers()
        {
            var allUsers = this.Data.Users.All()
                .Include(u => u.Tweets)
                .Include("Tweets.Tweet")
                .Include("Tweets.Tweet.Category")
                .Include("Tweets.Favorites")
                .Include("Favorites.UserTweets.Tweet")
                .Include("Favorites.UserTweets.User")
                .Include(u => u.Notifications)
                .Include("Notifications.Notification")
                .Include(u => u.Messages)
                .Include(u => u.Groups)
                .OrderBy(u => u.UserName)
                .Select(UserViewModel.CreateUser);

            if (allUsers == null)
            {
                return this.HttpNotFound("non existing users");
            }

            return this.View(allUsers);      
        }

        [Authorize]
        public ActionResult UserNotifications()
        {
            var allNotifications = this.Data.Users.All()
                .Include(u => u.Tweets)
                .Include("Tweets.Tweet")
                .Include("Tweets.Tweet.Category")
                .Include("Tweets.Favorites")
                .Include("Favorites.UserTweets.Tweet")
                .Include("Favorites.UserTweets.User")
                .Include(u => u.Notifications)
                .Include("Notifications.Notification")
                .Include(u => u.Messages)
                .Include(u => u.Groups)
                .Where(u => u.UserName == this.UserProfile.UserName)
                .Select(UserViewModel.CreateUser)
                .FirstOrDefault();

            return this.View(allNotifications);
        }

        [Authorize]
        public ActionResult NotificationDetails(int id)
        {
            var user = this.Data.Users.Find(this.UserProfile.Id);
            if (user == null)
            {
                return this.HttpNotFound("non existing user");
            }

            var userNotification = user.Notifications.AsQueryable()
                    .Include(un => un.Notification)
                    .Where(un => un.Notification.Id == id)
                    .Select(NotificationViewModel.Create)
                    .FirstOrDefault();
            if (userNotification == null)
            {
                return this.HttpNotFound();
            }

            return this.View(userNotification);
        }

        [Authorize]
        public ActionResult DeleteNotificationFromUser(int id)
        {
            var userNotification = this.Data.UserNotifications.All().Where(un => un.Id == id).FirstOrDefault();
            if (userNotification == null)
            {
                return this.HttpNotFound();
            }

            userNotification.UserId = null;
            this.Data.SaveChanges();

            return this.View();
        }
    }
}
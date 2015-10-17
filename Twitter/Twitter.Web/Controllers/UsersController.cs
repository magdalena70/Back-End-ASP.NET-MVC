
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
                .Include("Tweets.Categories")
                .Include("Tweets.Tweet")
                .Include("Favorites.UserFavoriteTweets")
                .Include("Favorites.UserFavoriteTweets.Tweet")
                .Include("Favorites.UserFavoriteTweets.User")
                .Include(u => u.Groups)
                .Where(u => u.UserName == username)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    ContactInfo = u.ContactInfo,
                    AvatarUrl = u.AvatarUrl != null ? u.AvatarUrl : "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJNGKxDl-q0wRp-eKqFc1jzuKeGA_tldmvO71crqFQ8ptsqIjk",
                    TweetCount = u.Tweets.Count(),
                    Tweets = u.Tweets.AsQueryable()
                        .OrderByDescending(t => t.Tweet.SentToDate)
                        .ThenByDescending(t => t.Tweet.Id)
                        .Select(TweetViewModel.Create)
                })
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
                .Include("Tweets.Categories")
                .Include("Tweets.Tweet")
                .Include("Favorites.UserFavoriteTweets")
                .Include("Favorites.UserFavoriteTweets.Tweet")
                .Include("Favorites.UserFavoriteTweets.User")
                .Where(u => u.UserName == username)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    ContactInfo = u.ContactInfo,
                    AvatarUrl = u.AvatarUrl != null ? u.AvatarUrl : "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJNGKxDl-q0wRp-eKqFc1jzuKeGA_tldmvO71crqFQ8ptsqIjk",
                    TweetCount = u.Tweets.Count(),
                    UserTweets = u.Tweets.AsQueryable()
                        .OrderByDescending(t => t.Tweet.SentToDate)
                        .ThenByDescending(t => t.TweetId)
                        .Select(UserTweetViewModel.Create)
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
            var favorites = this.Data.Favorites.All().Where(f => f.Fan.UserName == username);
            var names = favorites.Select(f => f.UserTweet.Tweet.Title).ToList();

            return this.Content(string.Format("{0}", string.Join(", ", names)));
        }

        public ActionResult AllUsers()
        {
            var users = this.Data.Users.All();
            var names = users.Select(u => u.UserName).ToList();

            return this.Content(string.Format("{0}", string.Join(", ", names)));
            
        }
    }
}
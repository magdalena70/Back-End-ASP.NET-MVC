
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

    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            :base(data)
        {
        }

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
                    AvatarUrl = u.AvatarUrl,
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

        public ActionResult UserTweets(string username)
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
                    AvatarUrl = u.AvatarUrl,
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
    }
}
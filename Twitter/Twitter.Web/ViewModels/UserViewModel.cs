
namespace Twitter.Web.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Models;

    public class UserViewModel
    {
        public static Expression<Func<User, UserViewModel>> CreateUser
        {
            get
            {
                return u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    ContactInfo = u.ContactInfo,
                    AvatarUrl = u.AvatarUrl != null ?
                        u.AvatarUrl : 
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJNGKxDl-q0wRp-eKqFc1jzuKeGA_tldmvO71crqFQ8ptsqIjk",
                    TweetCount = u.Tweets.Count
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public int TweetCount { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public IEnumerable<TweetViewModel> Tweets { get; set; }

        public IEnumerable<UserTweetViewModel> UserTweets { get; set; }

        public static object Create { get; set; }

        public IQueryable<int> FavoriteTweetsCount { get; set; }
    }
}

namespace Twitter.Web.ViewModels
{
    using Antlr.Runtime.Misc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public IEnumerable<TweetViewModel> Tweets { get; set; }

        public IEnumerable<UserTweetViewModel> UserTweets { get; set; }

        public static object Create { get; set; }
    }
}
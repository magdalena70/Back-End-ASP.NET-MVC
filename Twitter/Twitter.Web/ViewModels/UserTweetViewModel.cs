
namespace Twitter.Web.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Models;

    public class UserTweetViewModel
    {
        public static Expression<Func<UserTweet, UserTweetViewModel>> CreateTweet
        {
            get
            {
                return ut => new UserTweetViewModel
                {
                    Id = ut.Id,
                    Title = ut.Tweet.Title,
                    Details = ut.Tweet.Details,
                    SentToDate = ut.Tweet.SentToDate.ToString(),
                    ImageUrl = ut.Tweet.ImageUrl != null ?
                        ut.Tweet.ImageUrl :
                        "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQneHvAWxVhm0Pvh472jvvidVePRfupsn6Pm2ONRdENeM2IElpF",
                    CategoryName = ut.Tweet.Category.Name,
                    FavoriteCount = ut.Favorites.Count,
                    Fans = ut.Favorites.Select(f => f.Fan.UserName),
                    RetweetsCount = ut.Retweets.Count,
                    Retweets = ut.Retweets.AsQueryable().Select(RetweetViewModel.Create)
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string SentToDate { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public int FavoriteCount { get; set; }

        public IEnumerable<string> Fans { get; set; }

        public int RetweetsCount { get; set; }

        public IQueryable<RetweetViewModel> Retweets { get; set; }
    }
}
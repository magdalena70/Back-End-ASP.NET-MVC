
namespace Twitter.Web.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class FavoritesViewModel
    {
        public static Expression<Func<Favorite, FavoritesViewModel>> Create
        {
            get
            {
                return f => new FavoritesViewModel
                {
                    Id = f.UserTweet.TweetId,
                    Title = f.UserTweet.Tweet.Title,
                    Details = f.UserTweet.Tweet.Details,
                    Category = f.UserTweet.Tweet.Category.Name,
                    SentToDate = f.UserTweet.Tweet.SentToDate,
                    Author = f.UserTweet.Author.UserName,
                    ImageUrl = f.UserTweet.Tweet.ImageUrl != null ?
                        f.UserTweet.Tweet.ImageUrl : 
                        "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSocvbAzvI7P8li5WO4jQC2YfgC2q2sL6W7Bws8VTrWN4veLKBFug",
                    FavoriteCount = f.UserTweet.Tweet.Fans.Count
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string Category { get; set; }

        public DateTime SentToDate { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public int FavoriteCount { get; set; }
    }
}
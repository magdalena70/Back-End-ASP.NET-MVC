
namespace Twitter.Web.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class TweetViewModel
    {
        public static Expression<Func<UserTweet, TweetViewModel>> Create
        {
            get
            {
                return t => new TweetViewModel
                {
                    Id = t.Id,
                    Title = t.Tweet.Title,
                    Details = t.Tweet.Details,
                    SentToDate = t.Tweet.SentToDate,
                    ImageUrl = t.Tweet.ImageUrl != null ?
                        t.Tweet.ImageUrl :
                        "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSocvbAzvI7P8li5WO4jQC2YfgC2q2sL6W7Bws8VTrWN4veLKBFug",
                    CategoryName = t.Tweet.Category.Name,
                    Author = t.Author.UserName,
                    AuthorId = t.AuthorId,
                    FavoriteCount = t.Favorites.Count,
                    Fans = t.Favorites.Select(f => f.Fan.UserName)
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public string CategoryName { get; set; }

        public int FavoriteCount { get; set; }

        public IEnumerable<string> Fans { get; set; }
    }
}
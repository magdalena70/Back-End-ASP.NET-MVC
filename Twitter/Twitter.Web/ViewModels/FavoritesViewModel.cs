
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
                    AuthorAvatar = f.UserTweet.Author.AvatarUrl != null ?
                        f.UserTweet.Author.AvatarUrl :
                        "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR_N1xlcGHULtzO3ylNBd0MPc65_e4_L2OcKH_okfIm9HUN3R8i",
                    ImageUrl = f.UserTweet.Tweet.ImageUrl != null ?
                        f.UserTweet.Tweet.ImageUrl :
                        "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcT0aazpbpOkO9oN0-gWrS0msOKNImb2OlR6PqSbNKES4d3Ly8YA",
                    FavoriteCount = f.UserTweet.Tweet.Fans.Count,
                    RetweetsCount = f.UserTweet.Retweets.Count
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

        public string AuthorAvatar { get; set; }

        public int RetweetsCount { get; set; }
    }
}
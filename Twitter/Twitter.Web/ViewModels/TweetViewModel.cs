
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
                    SentToDate = t.Tweet.SentToDate.ToString(),
                    ImageUrl = t.Tweet.ImageUrl != null ?
                        t.Tweet.ImageUrl :
                        "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQbfedSLb3yJtD5bvTAtHzEYno3GrsA4sqE-Sijy9a66swuK2pYXA",
                    CategoryName = t.Tweet.Category.Name,
                    Author = t.Author.UserName,
                    AuthorAvatar = t.Author.AvatarUrl != null ?
                        t.Author.AvatarUrl :
                        "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR_N1xlcGHULtzO3ylNBd0MPc65_e4_L2OcKH_okfIm9HUN3R8i",
                    AuthorId = t.AuthorId,
                    RetweetsCount = t.Retweets.Count,
                    Retweets = t.Retweets.AsQueryable().Select(RetweetViewModel.Create),
                    FavoriteCount = t.Favorites.Count,
                    Fans = t.Favorites.Select(f => f.Fan.UserName)
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string SentToDate { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public string CategoryName { get; set; }

        public int FavoriteCount { get; set; }

        public IEnumerable<string> Fans { get; set; }

        public int RetweetsCount { get; set; }

        public IEnumerable<RetweetViewModel> Retweets { get; set; }

        public string AuthorAvatar { get; set; }
    }
}
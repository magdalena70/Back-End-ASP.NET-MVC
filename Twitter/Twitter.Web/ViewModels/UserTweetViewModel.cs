﻿
namespace Twitter.Web.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class UserTweetViewModel
    {
        public static Expression<Func<UserTweet, UserTweetViewModel>> Create
        {
            get
            {
                return ut => new UserTweetViewModel
                {
                    Id = ut.Id,
                    Title = ut.Tweet.Title,
                    Details = ut.Tweet.Details,
                    SentToDate = ut.Tweet.SentToDate,
                    ImageUrl = ut.Tweet.ImageUrl != null ? ut.Tweet.ImageUrl : "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSocvbAzvI7P8li5WO4jQC2YfgC2q2sL6W7Bws8VTrWN4veLKBFug",
                    CategoryName = ut.Tweet.Category.Name
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }
    }
}
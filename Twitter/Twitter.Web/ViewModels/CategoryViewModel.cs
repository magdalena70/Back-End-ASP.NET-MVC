
namespace Twitter.Web.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class CategoryViewModel
    {
        public static Expression<Func<Category, CategoryViewModel>> Create
        {
            get
            {
                return c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    TweetsCount = c.Tweets.Count
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int TweetsCount { get; set; }
    }
}
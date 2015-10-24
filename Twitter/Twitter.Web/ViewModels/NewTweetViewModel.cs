
namespace Twitter.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class NewTweetViewModel
    {
        public static Expression<Func<Tweet, NewTweetViewModel>> Create
        {
            get
            {
                return t => new NewTweetViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Details = t.Details,
                    ImageUrl = t.ImageUrl != null ?
                        t.ImageUrl :
                        "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSocvbAzvI7P8li5WO4jQC2YfgC2q2sL6W7Bws8VTrWN4veLKBFug",
                    SentToDate = t.SentToDate,
                    Category = t.Category.Name
                };
            }
        }

        public string Title { get; set; }

        public string Details { get; set; }

        public string ImageUrl { get; set; }

        public System.DateTime SentToDate { get; set; }

        public string Category { get; set; }

        public int Id { get; set; }
    }
}
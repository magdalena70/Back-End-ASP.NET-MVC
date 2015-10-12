
namespace Twitter.Web.ViewModels
{
    using System;

    public class TweetViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public int CategoryId { get; set; }
    }
}
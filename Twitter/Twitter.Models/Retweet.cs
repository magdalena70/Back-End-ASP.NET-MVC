
namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Retweet
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string RetweeterId { get; set; }

        public virtual User Retweeter { get; set; }

        public int userTweetId { get; set; }

        public virtual UserTweet UserTweet { get; set; }
    }
}

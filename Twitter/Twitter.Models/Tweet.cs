
namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        private ICollection<UserTweet> fans;

        public Tweet()
        {
            this.fans = new HashSet<UserTweet>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<UserTweet> Fans
        {
            get { return this.fans; }
            set { this.fans = value; }
        }
    }
}

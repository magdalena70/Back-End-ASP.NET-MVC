using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroBlogWeb.Models
{
    public class Tweet
    {
        public Tweet()
        {
            this.Retweets = new HashSet<Tweet>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(160)]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime? TimeOfPosting { get; set; }

        public virtual User Author { get; set; }

        public virtual User Fen { get; set; }

        public virtual ICollection<Tweet> Retweets { get; set; }

    }
}

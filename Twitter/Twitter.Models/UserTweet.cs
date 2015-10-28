
namespace Twitter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserTweet
    {
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int TweetId { get; set; }

        public virtual Tweet Tweet { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<Retweet> Retweets { get; set; }
    }
}

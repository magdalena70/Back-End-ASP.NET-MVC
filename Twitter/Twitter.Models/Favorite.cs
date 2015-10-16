
namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Favorite
    {
        public int Id { get; set; }

        [Required]
        public string FanId { get; set; }

        public virtual User Fan { get; set; }

        public int userTweetId { get; set; }

        public virtual UserTweet UserTweet { get; set; }
    }
}

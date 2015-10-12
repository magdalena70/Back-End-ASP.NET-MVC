
namespace Twitter.Models
{
    public class UserFavouriteTweet
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TweetId { get; set; }

        public virtual Tweet Tweet { get; set; }
    }
}

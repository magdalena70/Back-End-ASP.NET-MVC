
namespace Twitter.Data
{
    using System.Data.Entity;
    using Twitter.Models;

    public interface ITwitterDbContext
    {
        IDbSet<Group> Groups { get; set; }

        IDbSet<Tweet> Tweets { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        IDbSet<AdministrationLog> AdministrationLogs { get; set; }

        IDbSet<UserFavouriteTweet> FavoriteTweets { get; set; }

        int SaveChanges();
    }
}

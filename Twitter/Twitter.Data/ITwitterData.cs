
namespace Twitter.Data
{
    using Twitter.Data.Repositories;
    using Twitter.Models;

    public interface ITwitterData // about Unit of Work
    {
        IRepository<User> Users { get; }

        IRepository<AdministrationLog> AdministrationLogs { get; }

        IRepository<Group> Groups { get; }

        IRepository<Tweet> Tweets { get; }

        IRepository<Favorite> Favorites { get; }

        IRepository<Message> Messages { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<Category> Categories { get; }

        int SaveChanges();
    }
}

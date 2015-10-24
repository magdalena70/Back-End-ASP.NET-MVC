
namespace Twitter.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Twitter.Models;
    using Migrations;

    public class TwitterDbContext : IdentityDbContext<User>, ITwitterDbContext
    {
        public TwitterDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterDbContext, Configuration>());
        }

        public static TwitterDbContext Create()
        {
            return new TwitterDbContext();
        }

        public IDbSet<AdministrationLog> AdministrationLogs { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<UserTweet> AllTweets { get; set; }

        public IDbSet<Favorite> Favorites { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<UserNotification> UserNotifications { get; set; }

        public IDbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasRequired(x => x.Owner)
                .WithOptional()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(x => x.Recipient)
                .WithOptional()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(x => x.Sender)
                .WithOptional()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tweet>()
                .HasRequired(x => x.Category)
                .WithMany(x => x.Tweets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Favorite>()
                .HasRequired(x => x.UserTweet)
                .WithMany(x => x.Favorites)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

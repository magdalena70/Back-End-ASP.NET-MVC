
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

        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<UserTweet> AllTweets { get; set; }

        public IDbSet<Retweet> Retweets { get; set; }

        public IDbSet<Favorite> Favorites { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<UserNotification> UserNotifications { get; set; }

        public IDbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Followers)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("FollowerUsers");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("FollowerUserId");
                });

            modelBuilder.Entity<User>()
                .HasMany(x => x.Followings)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("FollowingUsers");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("FollowingUserId");
                });

            modelBuilder.Entity<Tweet>()
                .HasRequired(x => x.Category)
                .WithMany(x => x.Tweets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Favorite>()
                .HasRequired(x => x.UserTweet)
                .WithMany(x => x.Favorites)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Retweet>()
                .HasRequired(x => x.UserTweet)
                .WithMany(x => x.Retweets)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

namespace MicroBlogWeb.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class MicroBlogWebContext : IdentityDbContext<User>
    {
        public MicroBlogWebContext()
            : base("MicroBlogWebContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Tweet> Tweets { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        public static MicroBlogWebContext Create()
        {
            return new MicroBlogWebContext();
        }
    }
}
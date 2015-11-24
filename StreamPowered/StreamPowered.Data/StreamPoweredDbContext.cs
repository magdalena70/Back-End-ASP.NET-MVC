
namespace StreamPowered.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using StreamPowered.Models;
    using System.Data.Entity;

    public class StreamPoweredDbContext : IdentityDbContext<User>
    {
        public StreamPoweredDbContext()
            : base("StreamPoweredConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Review> Reviews { get; set; }

        public virtual IDbSet<Genre> Genres { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public virtual IDbSet<Image> ImageUrls { get; set; }

        public static StreamPoweredDbContext Create()
        {
            return new StreamPoweredDbContext();
        }
    }
}

namespace MicroBlogWeb.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        public User()
        {
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.SentTweets = new HashSet<Tweet>();
            this.FavouriteTweets = new HashSet<Tweet>();
            this.SentMessages = new HashSet<Message>();
            this.ReceivedMessages = new HashSet<Message>();
            this.Notifications = new HashSet<Notification>();
        }

        public string Address { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<Tweet> SentTweets { get; set; }

        [InverseProperty("Fen")]
        public virtual ICollection<Tweet> FavouriteTweets { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<Message> SentMessages { get; set; }

        [InverseProperty("Recipient")]
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one 
            // defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager
                    .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

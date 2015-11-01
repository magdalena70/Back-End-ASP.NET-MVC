
namespace Twitter.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        private ICollection<UserTweet> tweets;
        private ICollection<UserNotification> notifications;
        private ICollection<Message> messages;
        private ICollection<User> followers;
        private ICollection<User> followings;

        public User()
        {
            this.tweets = new HashSet<UserTweet>();
            this.notifications = new HashSet<UserNotification>();
            this.messages = new HashSet<Message>();
            this.followers = new HashSet<User>();
            this.followings = new HashSet<User>();
            this.ContactInfo = new ContactInfo();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public virtual ICollection<UserTweet> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }

        public virtual ICollection<UserNotification> Notifications
        {
            get { return this.notifications; }
            set { this.notifications = value; }
        }

        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }

        public virtual ICollection<User> Followers
        {
            get { return this.followers; }
            set { this.followers = value; }
        }

        public virtual ICollection<User> Followings
        {
            get { return this.followings; }
            set { this.followings = value; }
        }
    }
}


namespace SportSystem.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        public virtual ICollection<Bet> Bets { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

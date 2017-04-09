using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Models.EntityModels
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Reviews = new HashSet<Review>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public decimal MoneySpentBalance { get; set; }

        public virtual Basket Basket { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Book> FavoriteBooks { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

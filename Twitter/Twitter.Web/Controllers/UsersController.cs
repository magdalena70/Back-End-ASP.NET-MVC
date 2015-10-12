
namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using Twitter.Data;
    using Twitter.Web.ViewModels;

    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index(string username)
        {
            var user = this.Data.Users.All()
                .Where(u => u.UserName == username)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    ContactInfo = u.ContactInfo,
                    AvatarUrl = u.AvatarUrl
                })
                .FirstOrDefault();
            if (user == null)
            {
                return this.HttpNotFound("non existing user");
            }

            return this.View(user);
        }
    }
}
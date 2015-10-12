
namespace Twitter.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Twitter.Data;
    using Twitter.Models;

    public abstract class BaseController : Controller
    {
        private ITwitterData data;
        private User userProfile; //access user if is logged in (by BeginExecute() - in requestContext contains user's data)

        protected BaseController(ITwitterData data)
        {
            this.Data = data;
        }

        protected BaseController(ITwitterData data, User userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected ITwitterData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);
                this.UserProfile = user; // set user to UserProfile
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}
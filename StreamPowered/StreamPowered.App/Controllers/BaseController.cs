
namespace StreamPowered.App.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Web.Routing;
    using System.Linq;
    using Data.UnitOfWork;
    using StreamPowered.Models;

    public class BaseController : Controller
    {
        public BaseController(IStreamPoweredData data)
        {
            this.Data = data;
        }

        public BaseController(IStreamPoweredData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public IStreamPoweredData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}
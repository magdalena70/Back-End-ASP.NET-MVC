
namespace Snippy.App.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Web.Routing;
    using System.Linq;
    using Snippy.Models;
    using Data.UnitOfWork;

    public class BaseController : Controller
    {
        public BaseController(ISnippyData data)
        {
            this.Data = data;
        }

        public BaseController(ISnippyData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public ISnippyData Data { get; private set; }

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
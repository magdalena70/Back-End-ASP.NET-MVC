using System.Linq;
using System.Web.Mvc;

namespace BookStore.App.Attributes
{
    public class CustomAttributeAuth : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var roles = Roles.Split(',');
            if (filterContext.HttpContext.Request.IsAuthenticated &&
                !roles.Any(filterContext.HttpContext.User.IsInRole))
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Shared/RoleErrorMessage.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
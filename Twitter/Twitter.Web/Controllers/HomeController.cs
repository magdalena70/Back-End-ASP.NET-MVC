
namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Twitter.Data;
    using System.Web.Mvc.Expressions;
    using Twitter.Web.ViewModels; // https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers - ASP.NET.MVC 5.2.3

    public class HomeController : BaseController // must install Ninject.MVC5 (to use constructor with parametеrs)
    {
        public HomeController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {

            return this.View();
        }

        public ActionResult About()
        {

            return this.RedirectToAction(x => x.Index());
        }
    }
}
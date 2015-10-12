
namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Data;
    using System.Web.Mvc.Expressions; // https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers - ASP.NET.MVC 5.2.3

    public class HomeController : BaseController // must install Ninject.MVC5 (to use constructor with parametеrs)
    {
        public HomeController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            //this.ViewBag.UserName = this.UserProfile.UserName;
            return this.View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            //return this.View();

            return this.RedirectToAction(x => x.Contact());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}
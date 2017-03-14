using System.Web.Mvc;

namespace MicroBlogWeb.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
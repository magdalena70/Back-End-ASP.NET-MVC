using System.Web.Mvc;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
using BookStore.Data;
using System.Web.Mvc;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        public ActionResult Index()
        {
            this.context.Database.Initialize(true);
            return View();
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
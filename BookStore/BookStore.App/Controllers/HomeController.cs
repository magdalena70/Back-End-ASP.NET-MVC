using BookStore.Data;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Web.Mvc;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        private HomeService homeService;

        public HomeController()
        {
            this.homeService = new HomeService(context);
        }

        public ActionResult Index()
        {
            //this.context.Database.Initialize(true);
            HomePageViewModel viewModel = this.homeService.GetHomePageViewModel();
            return View(viewModel);
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
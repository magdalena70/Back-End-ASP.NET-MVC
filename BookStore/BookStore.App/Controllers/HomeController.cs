using BookStore.Models.ViewModels.Home;
using BookStore.Services.Interfaces;
using System.Web.Mvc;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IHomeService homeService;

        public HomeController(IHomeService service)
        {
            this.homeService = service;
        }

        public ActionResult Index()
        {
            HomePageViewModel viewModel = this.homeService.GetHomePageViewModel();
            return View(viewModel);
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
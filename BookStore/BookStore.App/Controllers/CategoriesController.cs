using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Category;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class CategoriesController : Controller
    {
        private CategoryService categoryService;

        public CategoriesController()
        {
            this.categoryService = new CategoryService();
        }

        // GET: Categories/All
        public ActionResult All()
        {
            IEnumerable<AllCategoriesViewModel> viewModel = this.categoryService.GetAll();
            return View(viewModel);
        }

        // GET: Categories/BestSellers
        public ActionResult BestSellers()
        {
            CategoryViewModel viewModel = this.categoryService.GetBestSellers();

            if (viewModel == null)
            {
                this.TempData["Info"] = "No books in category: 'BestSellers'";
                return View();
            }

            return View(viewModel);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = "Incorect url";
                return RedirectToAction("Index", "Home");
            }

            CategoryViewModel viewModel = this.categoryService.GetCategoryDetails(id);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"There is no category with id: {id}.";
                return RedirectToAction("All", "Categories");
            }

            if (viewModel.Books.Count == 0)
            {
                this.TempData["Info"] = $"There are no books in category {viewModel.Name}.";
            }

            return View(viewModel);
        }

        // GET: Categories/BooksByCategory?categoryName=...
        [ActionName("BooksByCategory")]
        public ActionResult SearchBooksByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                this.TempData["Info"] = "Enter category name to find books.";
                return View();
            }

            CategoryViewModel viewModel = this.categoryService.GetCategoryByName(categoryName);
           
            if (viewModel == null)
            {
                this.TempData["Error"] = $"No books in category: '{categoryName}'";
                return View();
            }

            return View(viewModel);
        }
    }
}

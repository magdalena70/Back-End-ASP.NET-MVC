using System.Web.Mvc;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Category;
using System;
using BookStore.Services.Interfaces;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class CategoriesController : Controller
    {
        private ICategoryService categoryService;

        public CategoriesController(ICategoryService service)
        {
            this.categoryService = service;
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
                throw new Exception("Invalid URL - category's id can not be null");
            }

            CategoryViewModel viewModel = this.categoryService.GetCategoryDetails(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no category with id: { id }");
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
                throw new Exception("Invalid URL - category's name can not be null");
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

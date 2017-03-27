using System.Data.Entity;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using System.Collections.Generic;
using BookStore.Models.BindingModels;

namespace BookStore.App.Controllers
{
    public class CategoriesController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        private CategoryService categoryService;

        public CategoriesController()
        {
            this.categoryService = new CategoryService(context);
        }

        // GET: Categories
        [AllowAnonymous]
        public ActionResult All()
        {
            List<AllCategoriesViewModel> viewModel = this.categoryService.GetAll();
            return View(viewModel);
        }

        // GET: Categories/BestSellers
        [AllowAnonymous]
        public ActionResult BestSellers()
        {
            CategoryViewModel viewModel = this.categoryService.GetBestSellers();

            if (viewModel == null)
            {
                this.TempData["Error"] = "No books in category: 'BestSellers'";
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        // GET: Categories/Details/5
        [AllowAnonymous]
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
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        // GET: Categories/BooksByCategory?categoryName=...
        [AllowAnonymous]
        [ActionName("BooksByCategory")]
        public ActionResult SearchBooksByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                this.TempData["Error"] = "Enter category name to find books.";
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

        //-----------------for admin---------------------------//

        //for admin
        // GET: Categories/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //for admin
        // POST: Categories/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CategoryBindingModel bindingModel)
        {
            Category category = this.categoryService.CreateCategory(bindingModel);
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();

                this.TempData["Success"] = $"Created category {category.Name} successfully";
                return RedirectToAction("All", "Categories");
            }

            return View(category);
        }

        //for admin
        // GET: Categories/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = "Category's id can not be null.";
                return RedirectToAction("All", "Categories");
            }

            Category category = context.Categories.Find(id);
            if (category == null)
            {
                this.TempData["Error"] = "Тhere is no such category.";
                return RedirectToAction("All", "Categories");
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] CategoryBindingModel bindingModel)
        {
            Category category = this.categoryService.EditCategory(bindingModel);
            if (ModelState.IsValid)
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();

                this.TempData["Success"] = $"Edited category {category.Name} successfully";
                return RedirectToAction("Details", "Categories", new { id = category.Id});
            }

            return View(category);
        }

        //for admin
        // GET: Categories/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = "Category's id can not be null.";
                return RedirectToAction("All", "Categories");
            }

            Category category = context.Categories.Find(id);
            if (category == null)
            {
                this.TempData["Error"] = "Тhere is no such category.";
                return RedirectToAction("All", "Categories");
            }

            return View(category);
        }

        //for admin
        // POST: Categories/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.categoryService.DeleteCategory(id);
            this.TempData["Error"] = "Delited category successfully.";

            return RedirectToAction("All", "Categories");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

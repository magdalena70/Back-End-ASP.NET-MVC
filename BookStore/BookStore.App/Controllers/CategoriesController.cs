using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using System.Collections.Generic;

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
        public ActionResult All()
        {
            List<AllCategoriesViewModel> viewModel = this.categoryService.GetAll();
            return View(viewModel);
        }

        // GET: Categories/BestSellers
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

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }
        //--------------------------------------------------to do---------------------------//
        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
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

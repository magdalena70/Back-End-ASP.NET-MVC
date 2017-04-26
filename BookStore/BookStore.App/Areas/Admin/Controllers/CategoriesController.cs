using System.Net;
using System.Web.Mvc;
using BookStore.Models.EntityModels;
using BookStore.App.Attributes;
using BookStore.Models;
using System.Collections.Generic;
using BookStore.Models.BindingModels.Category;
using BookStore.Models.ViewModels.Category;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private CategoryService categoryService;

        public CategoriesController()
        {
            this.categoryService = new CategoryService();
        }

        // GET: Admin/Categories?categoryName=
        public ActionResult AllCategories(string categoryName)
        {
            IEnumerable<AllCategoriesViewModel> viewModel;
            if (categoryName == null)
            {
                viewModel = this.categoryService.GetAll();

            }
            else
            {
                viewModel = this.categoryService.GetAllByName(categoryName);
            }

            return View(viewModel);
        }

        // GET: Admin/Categories/Details/5
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
                this.TempData["Info"] = $"There are no books in category '{viewModel.Name}'.";
            }

            return View(viewModel);
        }

        // GET: Admin/Categories/Create
        public ActionResult AddCategories()
        {
            return View();
        }

        // POST: Admin/Categories/AddCategories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategories([Bind(Include = "Name")] AddCategoryBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                if (this.categoryService.IsCategoryExists(bindingModel.Name))
                {
                    this.TempData["Error"] = $"Category with name {bindingModel.Name} already exists";
                    return View(bindingModel);
                }

                this.categoryService.AddNewCategory(bindingModel);
                CategoryViewModel newCategory = this.categoryService.GetCurrentCategory(bindingModel.Name);
                this.TempData["Success"] = $"Category '{bindingModel.Name}' was created successfully.";

                return RedirectToAction("Details", "Categories", new { id = newCategory.Id });
            }

            return View(bindingModel);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditCategoryViewModel viewModel = this.categoryService.GetCategoryViewModel(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] EditCategoryBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.categoryService.EditCategory(bindingModel);

                this.TempData["Success"] = "This category was edited successfully.";
                return RedirectToAction("Details", "Categories", new { id = bindingModel.Id });
            }

            EditCategoryViewModel viewModel = this.categoryService.GetCategoryViewModel(bindingModel.Id);
            return View(viewModel);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeleteCategoryViewModel viewModel = this.categoryService.GetDeleteCategoryViewModel(id);
            if (viewModel == null)
            {
                return RedirectToAction("AllCategories", "Categories");
            }

            return View(viewModel);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string currCategoryName = this.categoryService.GetCategoryName(id);
            this.categoryService.DeleteCategory(id);

            this.TempData["Success"] = $"Category '{currCategoryName}' was removed successfully.";
            return RedirectToAction("AllCategories", "Categories");
        }
    }
}

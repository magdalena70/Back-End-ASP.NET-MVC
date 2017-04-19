using System.Net;
using System.Web.Mvc;
using BookStore.App.Attributes;
using BookStore.Services;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Author;
using BookStore.Models.BindingModels.Author;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class AuthorsController : Controller
    {
        private AuthorService authorService;

        public AuthorsController()
        {
            this.authorService = new AuthorService();
        }

        // GET: Admin/Authors?authorName=
        public ActionResult AllAuthors(string authorName)
        {
            IEnumerable<AuthorViewModel> viewModel;
            if (authorName == null)
            {
                viewModel = this.authorService.GetAll();
            }
            else
            {
                viewModel = this.authorService.GetAllByName(authorName);
            }

            return View(viewModel);
        }

        // GET: Admin/Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthorWithBooksViewModel viewModel = this.authorService.GetAuthorDetails(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: Admin/Authors/Create
        public ActionResult AddAuthor()
        {
            return View();
        }

        // POST: Admin/Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuthor([Bind(Include = "Id,FullName,Bio")] AddAuthorBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                if (this.authorService.IsAuthorExists(bindingModel.FullName))
                {
                    this.TempData["Error"] = $"Author with name {bindingModel.FullName} already exists";
                    return View(bindingModel);
                }

                this.authorService.AddAuthor(bindingModel);
                AuthorViewModel newAuthor = this.authorService.GetCurrentAuthor(bindingModel.FullName);
                
                this.TempData["Success"] = $"Author is created successfully";
                return RedirectToAction("Details", "Authors", new { id = newAuthor.Id});
            }

            return View(bindingModel);
        }

        // GET: Admin/Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Bio")] EditAuthorBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.authorService.EditAuthor(bindingModel);

                this.TempData["Success"] = "Author is edited successfully";
                return RedirectToAction("Details", "Authors", new { id = bindingModel.Id});
            }

            AuthorViewModel viewModel = this.authorService.GetCurrentAuthor(bindingModel.FullName);
            return View(viewModel);
        }

        // GET: Admin/Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.authorService.DeleteAuthor(id);
            this.TempData["Success"] = "Author was removed successfully.";
            return RedirectToAction("AllAuthors");
        }
    }
}

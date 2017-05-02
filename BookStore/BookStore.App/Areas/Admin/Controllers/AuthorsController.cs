using System.Net;
using System.Web.Mvc;
using BookStore.App.Attributes;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Author;
using BookStore.Models.BindingModels.Author;
using System;
using BookStore.Services.Interfaces;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class AuthorsController : Controller
    {
        private IAuthorService authorService;

        public AuthorsController(IAuthorService service)
        {
            this.authorService = service;
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
                throw new Exception($"Invalid URL - there is no author with id {id}");
            }

            return View(viewModel);
        }

        // GET: Admin/Authors/AddAuthor
        public ActionResult AddAuthor()
        {
            return View();
        }

        // POST: Admin/Authors/AddAuthor
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
                throw new Exception("Invalid URL - author's id can not be null");
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no author with id {id}");
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
                throw new Exception("Invalid URL - author's id can not be null");
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no author with id {id}");
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

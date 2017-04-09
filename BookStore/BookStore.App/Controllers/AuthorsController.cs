using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Collections.Generic;
using BookStore.Models.EntityModels;

namespace BookStore.App.Controllers
{
    public class AuthorsController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        private AuthorService authorService;

        public AuthorsController()
        {
            this.authorService = new AuthorService(context);
        }

        // GET: Authors
        [AllowAnonymous]
        public ActionResult All()
        {
            IEnumerable<AllAuthorsViewModel> viewModel = this.authorService.GetAll();
            return View(viewModel);
        }

        // GET: Authors/Details/id
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                this.TempData["Error"] = "Invalid url.";
                return RedirectToAction("All", "Authors");
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"There is no author whit id: {id}";
                return RedirectToAction("All", "Authors");
            }

            return View(viewModel);
        }

        // GET: Authors/BooksByAuthor?authorName=...
        [ActionName("BooksByAuthor")]
        public ActionResult BooksByAuthorFullName(string authorName)
        {
            if (string.IsNullOrEmpty(authorName))
            {
                this.TempData["Error"] = "Enter author's name to find books.";
                return View();
            }

            AuthorWithBooksViewModel viewModel = this.authorService.GetAuthorWithBooks(authorName);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"No author whith name: '{authorName}'";
                return View();
            }

            return View(viewModel);
        }

        //-----------------for admin---------------------------//

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Bio")] Author author)
        {
            if (ModelState.IsValid)
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = context.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Bio")] Author author)
        {
            if (ModelState.IsValid)
            {
                context.Entry(author).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = context.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = context.Authors.Find(id);
            context.Authors.Remove(author);
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

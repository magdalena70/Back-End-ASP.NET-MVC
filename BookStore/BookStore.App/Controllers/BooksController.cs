using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using BookStore.Models.ViewModels;
using System.Collections.Generic;
using System;

namespace BookStore.App.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        private BookService bookService;

        public BooksController()
        {
            this.bookService = new BookService(context);
        }

        // GET: Books/NewBooks
        public ActionResult NewBooks()
        {
            IEnumerable<BooksViewModel> viewModel = this.bookService.GetNewBooks();
            if (viewModel.Count() == 0)
            {
                this.TempData["Info"] = "No books.";
                //return View();
            }
            return View(viewModel);
        }

        //Get: Books/Promotions
        public ActionResult Promotions()
        {
            return View(context.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (id == null)
            {
                this.TempData["Error"] = "Incorrect url. Book id is required.";
                 return RedirectToLocal(returnUrl);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookDetailsViewModel viewModel = this.bookService.GetDetails(id);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"There is no book with id: {id}.";
                return RedirectToLocal(returnUrl);
            }

            return View(viewModel);
        }

        // GET: Books/BooksByTitle?bookTitle=...
        [ActionName("BooksByTitle")]
        public ActionResult SearchBooksByTitle(string bookTitle)
        {
            if (string.IsNullOrEmpty(bookTitle))
            {
                this.TempData["Error"] = "Incorrect url.Enter book's title to find books.";
                return View();
            }

            IEnumerable<BooksViewModel> viewModel = this.bookService.GetBooksByTitle(bookTitle);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"No book with title: '{bookTitle}'";
                return View();
            }

            return View(viewModel);
        }

        //to do-----------------for admin------------------------------//

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ImageUrl,Language,Description,Price,Quantity,NumberOfPages,IssueDate,ISBN,UpRating,DownRating")] Book book)
        {
            if (ModelState.IsValid)
            {
                context.Books.Add(book);
                context.SaveChanges();
                return RedirectToAction("NewBooks", "Books");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ImageUrl,Language,Description,Price,Quantity,NumberOfPages,IssueDate,ISBN,UpRating,DownRating")] Book book)
        {
            if (ModelState.IsValid)
            {
                context.Entry(book).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Details/" + book.Id, "Books");
            }

            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = context.Books.Find(id);
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

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

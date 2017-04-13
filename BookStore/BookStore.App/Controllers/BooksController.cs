using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Services;
using BookStore.Models.ViewModels;
using System.Collections.Generic;
using System;
using BookStore.Models.EntityModels;

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
            }

            return View(viewModel);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (id == null)
            {
                this.TempData["Error"] = "Incorrect url. Book id is required.";
                 return RedirectToAction("Index", "Home");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookDetailsViewModel viewModel = this.bookService.GetDetails(id);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"There is no book with id: {id}.";
                return RedirectToAction("Index", "Home");
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
    }
}

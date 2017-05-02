using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Book;
using System;
using Microsoft.AspNet.Identity;
using BookStore.Services.Interfaces;

namespace BookStore.App.Controllers
{
    public class BooksController : Controller
    {
        private IBookService bookService;

        public BooksController(IBookService service)
        {
            this.bookService = service;
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
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("Incorrect url. Book id can not be null.");
            }

            string currUserId = User.Identity.GetUserId();
            BookDetailsViewModel viewModel = this.bookService.GetDetails(id, currUserId);
            if (viewModel == null)
            {
                throw new Exception($"There is no book with id: {id}.");
            }

            return View(viewModel);
        }

        // GET: Books/BooksByTitle?bookTitle=...
        [ActionName("BooksByTitle")]
        public ActionResult SearchBooksByTitle(string bookTitle)
        {
            if (string.IsNullOrEmpty(bookTitle))
            {
                throw new Exception("Invalid URL - book's title can not be null");
            }

            IEnumerable<BooksViewModel> viewModel = this.bookService.GetBooksByTitle(bookTitle);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no book with title {bookTitle}");
            }

            return View(viewModel);
        }
    }
}

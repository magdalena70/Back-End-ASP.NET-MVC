using System.Net;
using System.Web.Mvc;
using BookStore.Models.EntityModels;
using BookStore.App.Attributes;
using BookStore.Services;
using BookStore.Models.ViewModels.Book;
using System.Collections.Generic;
using BookStore.Models.BindingModels.Book;
using System;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class BooksController : Controller
    {
        private BookService bookService;

        public BooksController()
        {
            this.bookService = new BookService();
        }

        // GET: Admin/Books
        public ActionResult AllBooks(int page = 1, int count = 3)
        {
            IEnumerable<AllBooksViewModel> viewModel = this.bookService.GetAll(page, count);
            int booksCount = this.bookService.GetAllBooksCount();
            if (booksCount == 0)
            {
                this.TempData["Info"] = "No books";
            }

            this.ViewBag.TotalPages = (booksCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            return View(viewModel);
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("Invalid URL - book's id can not be null");
            }

            BookDetailsViewModel viewModel = this.bookService.GetDetails(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no book with id {id}");
            }

            return View(viewModel);
        }

        //POST: Admin/Books/Deails/5
        [HttpPost, ActionName("AddAuthorToBook")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuthorToBook([Bind(Include = "Id,SelectAuthors")] AddAuthorToBookBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.bookService.AddAuthorToBook(bindingModel);
                this.TempData["Success"] = "Success";
                return RedirectToAction("Details", "Books", new { id = bindingModel.Id });
            }

            this.TempData["Error"] = "Error";
            return RedirectToAction("Details", "Books", new { id = bindingModel.Id });

        }

        //POST: Admin/Books/Deails/5
        [HttpPost, ActionName("AddCategoryToBook")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategoryToBook([Bind(Include = "Id,SelectCategories")] AddCategoryToBookBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.bookService.AddCategoryToBook(bindingModel);
                this.TempData["Success"] = "Success";
                return RedirectToAction("Details", "Books", new { id = bindingModel.Id });
            }

            this.TempData["Error"] = "Error";
            return RedirectToAction("Details", "Books", new { id = bindingModel.Id });

        }

        // GET: Admin/Books/AddBook
        public ActionResult AddBook()
        {
            AddBookViewModel viewModel = this.bookService.GetAddBookViewModel();
            return View(viewModel);
        }

        // POST: Admin/Books/AddBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook([Bind(Include = "Id,Title,ImageUrl,Language,Description,Price,Quantity,NumberOfPages,IssueDate,ISBN,Categories, Authors")] AddBookBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.bookService.AddBook(bindingModel);
                Book newBook = this.bookService.GetNewBooks(bindingModel.Title, bindingModel.ISBN);

                this.TempData["Success"] = $"Book {bindingModel.Title} was added successfully.";
                return RedirectToAction("Details", "Books", new { id = newBook.Id});
            }

            return View(bindingModel);
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("Invalid URL - book's id can not be null");
            }

            EditBookViewModel viewModel = this.bookService.GetEditBookViewModel(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no book with id {id}");
            }

            return View(viewModel);
        }

        // POST: Admin/Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ImageUrl,Language,Description,Price,Quantity,NumberOfPages,IssueDate,ISBN")] EditBookBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.bookService.GetEditBook(bindingModel);
                
                return RedirectToAction("Details", "Books", new { id = bindingModel.Id});
            }

            return View(bindingModel);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("Invalid URL - book's id can not be null");
            }

            DeleteBookViewModel viewModel = this.bookService.GetDeliteBookViewModel(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no book with id {id}");
            }

            return View(viewModel);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = this.bookService.GetCurrentBook(id);
            string bookTitle = book.Title;
            this.bookService.DeleteBook(book);

            this.TempData["Success"] = $"Book {bookTitle} was removed successfully.";
            return RedirectToAction("AllBooks", "Books");
        }
    }
}

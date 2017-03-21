using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using System;

namespace BookStore.App.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Books/NewBooks
        public ActionResult NewBooks()
        {
            var newBooks = db.Books
                .Where(b => b.IssueDate.Year == DateTime.Now.Year)
                .ToList();


            return View(newBooks);
        }

        //Get: Books/Promotions
        public ActionResult Promotions()
        {
            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/BooksByTitle?bookTitle=...
        [ActionName("BooksByTitle")]
        public ActionResult SearchBooksByTitle(string bookTitle)
        {
            if (string.IsNullOrEmpty(bookTitle))
            {
                this.TempData["Error"] = "Enter book's title to find books.";
                return View();
            }

            var books = db.Books
                .Where(b => b.Title.Contains(bookTitle));
            if (books.Count() == 0)
            {
                this.TempData["Error"] = $"No book with title: '{bookTitle}'";
                return View();
            }

            return View(books);
        }

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
                db.Books.Add(book);
                db.SaveChanges();
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
            Book book = db.Books.Find(id);
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
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
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

            Book book = db.Books.Find(id);
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
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNet.Identity;

namespace BookStore.App.Controllers
{
    public class UsersController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Basket);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult UserProfile()
        {
            string registeredUserId = User.Identity.GetUserId();
            User user = db.Users.Find(registeredUserId);
            if (user == null)
            {
                this.TempData["Error"] = "Log in, please!";
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        // GET: Users/FavoriteBooks
        public ActionResult FavoriteBooks()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user);
        }

        // POST: Users/FavoriteBooks
        [HttpPost, ActionName("FavoriteBooks")]
        [ValidateAntiForgeryToken]
        public ActionResult AddBookToFavoriteBooks([Bind(Include = "Id")] Book book)
        {
            User currUser = db.Users.Find(User.Identity.GetUserId());
            Book currBook = db.Books.Find(book.Id);

            currUser.FavoriteBooks.Add(currBook);
                db.SaveChanges();
                return RedirectToAction("FavoriteBooks", "Users");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Baskets, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Baskets, "Id", "Id", user.Id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Baskets, "Id", "Id", user.Id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Baskets, "Id", "Id", user.Id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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

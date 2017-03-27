using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNet.Identity;
using BookStore.Services;
using System;
using BookStore.Data;
using BookStore.Models.BindingModels;
using System.Linq;

namespace BookStore.App.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        private UserService userService;

        public UsersController()
        {
            this.userService = new UserService(context);
        }

        private User GetCurrentUser()
        {
            string currUserId = User.Identity.GetUserId();
            User currentUser = this.context.Users.Find(currUserId);
            return currentUser;
        }

        // GET: Users/UserProfile
        public ActionResult UserProfile()
        {
            User currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                this.TempData["Error"] = "Log in, please!";
                return RedirectToAction("Login", "Account");
            }

            UserProfileViewModel viewModel = this.userService.GetUserProfileViewModel(currentUser);
            return View(viewModel);
        }

        // GET: Users/FavoriteBooks
        public ActionResult FavoriteBooks()
        {
            var currentUser = GetCurrentUser();
            UserFavoriteBooksViewModel viewModel = this.userService.GetFavorite(currentUser);
            return View(viewModel);
        }

        // POST: Users/FavoriteBooks
        [Authorize]
        [HttpPost, ActionName("FavoriteBooks")]
        [ValidateAntiForgeryToken]
        public ActionResult AddBookToFavoriteBooks([Bind(Include = "Id")] FavoriteBookBindingModel book)
        {
            User currentUser = GetCurrentUser();
            this.userService.AddBookToFavoriteBooks(currentUser, book.Id);
            
            return RedirectToAction("FavoriteBooks", "Users");
        }

        // POST: Users/RemoveFromFavorite
        [Authorize]
        [HttpPost, ActionName("RemoveFromFavorite")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBookFromFavoriteBooks([Bind(Include = "Id")] FavoriteBookBindingModel book)
        {
            User currentUser = GetCurrentUser();
            this.userService.RemoveBookFromFavoriteBooks(currentUser, book.Id);

            this.TempData["Success"] = $"You removed one book from Favorite Books.";
            return RedirectToAction("FavoriteBooks", "Users");
        }

        // GET: Users/EditProfile
        [Authorize]
        public ActionResult EditProfile()
        {
            User currentUser = this.GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Home");

            }

            EditUserProfileViewModel viewModel = this.userService.GetEditUserProfileViewModel(currentUser);
            return View(viewModel);
        }

        // POST: Users/EditProfile
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditUserProfileBindingModel bindingModel)
        {
            User currentUser = this.GetCurrentUser();
            User editedUser = this.userService.EditUserProfile(currentUser, bindingModel);

            this.context.Entry(editedUser).State = EntityState.Modified;
            this.context.SaveChanges();
            return RedirectToAction("UserProfile", "Users");
        }

        //-------------------to do for admin---------------------------------//

        //// GET: Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Id = new SelectList(db.Baskets, "Id", "Id");
        //    return View();
        //}

        // POST: Users/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Id = new SelectList(db.Baskets, "Id", "Id", user.Id);
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

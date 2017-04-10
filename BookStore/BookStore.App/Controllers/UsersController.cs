using System.Data.Entity;
using System.Web.Mvc;
using BookStore.Models.ViewModels;
using Microsoft.AspNet.Identity;
using BookStore.Services;
using BookStore.Data;
using BookStore.Models.BindingModels;
using BookStore.Models.EntityModels;

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
        [HttpPost, ActionName("FavoriteBooks")]
        [ValidateAntiForgeryToken]
        public ActionResult AddBookToFavoriteBooks([Bind(Include = "Id")] FavoriteBookBindingModel book)
        {
            User currentUser = GetCurrentUser();
            this.userService.AddBookToFavoriteBooks(currentUser, book.Id);
            
            return RedirectToAction("FavoriteBooks", "Users");
        }

        // POST: Users/RemoveFromFavorite
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        context.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

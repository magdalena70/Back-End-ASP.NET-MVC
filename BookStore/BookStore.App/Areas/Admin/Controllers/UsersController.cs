using System.Net;
using System.Web.Mvc;
using BookStore.Services;
using BookStore.Models.ViewModels.User;
using System.Collections.Generic;

namespace BookStore.App.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private UserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }

        // GET: Admin/Users/AllUsers
        public ActionResult AllUsers()
        {
            IEnumerable<AllUsersViewModel> viewModel = this.userService.GetAll();
            if (viewModel == null)
            {
                return View();
            }
            
            return View(viewModel);
        }

        // GET: Admin/Users/Details/username=
        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserDetailsViewModel viewModel = this.userService.GetUserDetails(username);
            
            if (viewModel == null)
            {
                this.TempData["Error"] = $"There is no user with username {username}.";
                return View();
            }

            return View(viewModel);
        }

        // POST: Admin/Users/Details/username=
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserMoneySpentBalance([Bind(Include = "Id,MoneySpentBalance,UserName")] EditUserMoneySpentBalanceBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.userService.EditUserMoneySpentBalance(bindingModel);       
                return RedirectToAction("Details", "Users", new { username = bindingModel.UserName});
            }

            return View(bindingModel);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string username)
        {
            this.userService.DeleteUser(username);
            this.TempData["Success"] = $"User {username} was removed successfully.";
            return RedirectToAction("AllUsers");
        }
    }
}

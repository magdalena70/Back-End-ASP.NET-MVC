using BookStore.Data;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private BookStoreContext context;

        public AdminController()
        {
            this.context = new BookStoreContext();
        }

        // GET: Admin/Roles
        public ActionResult AssignRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.context));
            var roles = this.context.Roles
                .Select(r => new SelectListItem()
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .ToList();
            var users = this.context.Users
                .Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = u.UserName
                })
                .ToList();

            ViewBag.Roles = roles;
            ViewBag.Users = users;

            //RoleViewModel viewModel = new RoleViewModel()
            //{
            //    Roles = Role,
            //    Users = users
            //};

            return View();
        }

        [HttpPost]
        public ActionResult AssignRoles(RoleViewModel viewModel)
        {
            var userManager = new UserManager<User>(new UserStore<User>(this.context));
            var user = userManager.FindById(viewModel.Users);
            userManager.AddToRole(viewModel.Users, viewModel.Roles);

            return RedirectToAction("AssignRoles", "Admin");
        }
    }
}
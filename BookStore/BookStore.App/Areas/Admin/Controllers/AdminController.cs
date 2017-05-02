using BookStore.App.Attributes;
using BookStore.Models.BindingModels.Admin;
using BookStore.Models.ViewModels.Admin;
using BookStore.Services.Interfaces;
using System.Web.Mvc;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminService adminService;

        public AdminController(IAdminService service)
        {
            this.adminService = service;
        }

        // GET: Admin/AssignRoles
        [HttpGet]
        public ActionResult AssignRoles()
        {
            AssignRolesViewModel viewModel = this.adminService.GetAssignRolesViewModel();
            return View(viewModel);
        }

        // POST: Admin/AssignRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRoles(AssignRolesBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.adminService.AssignRoles(bindingModel);
                this.TempData["Success"] = "Success";
                return RedirectToAction("AllUsers", "Users");
                
            }

            return View(bindingModel);
        }
    }
}
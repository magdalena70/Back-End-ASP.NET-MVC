using BookStore.Models.ViewModels.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;
using BookStore.Models.BindingModels.Admin;
using BookStore.Models.EntityModels;

namespace BookStore.Services
{
    public class AdminService: Service
    {
        public AssignRolesViewModel GetAssignRolesViewModel()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.Context));
            var roles = this.Context.Roles
                .Select(r => new SelectListItem()
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .ToList();
            var users = this.Context.Users
                .Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = u.UserName
                })
                .ToList();

            AssignRolesViewModel viewModel = new AssignRolesViewModel()
            {
                Roles = roles,
                Users = users
            };

            return viewModel;
        }

        public void AssignRoles(AssignRolesBindingModel bindingModel)
        {
            var userManager = new UserManager<User>(new UserStore<User>(this.Context));
            var user = userManager.FindById(bindingModel.Users);
            userManager.AddToRole(bindingModel.Users, bindingModel.Roles);
        }
    }
}

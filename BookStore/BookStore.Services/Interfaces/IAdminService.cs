using BookStore.Models.BindingModels.Admin;
using BookStore.Models.ViewModels.Admin;

namespace BookStore.Services.Interfaces
{
    public interface IAdminService
    {
        void AssignRoles(AssignRolesBindingModel bindingModel);
        AssignRolesViewModel GetAssignRolesViewModel();
    }
}
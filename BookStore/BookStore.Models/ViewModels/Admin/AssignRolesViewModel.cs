using System.Collections.Generic;
using System.Web.Mvc;

namespace BookStore.Models.ViewModels.Admin
{
    public class AssignRolesViewModel
    {
        public ICollection<SelectListItem> Users { get; set; }

        public ICollection<SelectListItem> Roles { get; set; }
    }
}

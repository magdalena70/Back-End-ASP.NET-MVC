using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.User
{
    public class AllUsersViewModel
    {
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}

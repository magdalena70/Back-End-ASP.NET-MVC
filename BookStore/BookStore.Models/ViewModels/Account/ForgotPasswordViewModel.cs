using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email-Address")]
        public string Email { get; set; }
    }
}

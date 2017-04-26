using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.User
{
    public class UserPurchaseViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public int PurchasesCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal MoneySpentBalance { get; set; }
    }
}

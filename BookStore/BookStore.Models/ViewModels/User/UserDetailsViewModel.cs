using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Purchase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.User
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal MoneySpentBalance { get; set; }

        public BasketViewModel Basket { get; set; }

        [Display(Name = "Favorite Books")]
        public List<BooksViewModel> FavoriteBooks { get; set; }

        public List<AllPurchasesViewModel> Purchases { get; set; }
    }
}

using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.Category;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }

        public BasketViewModel Basket { get; set; }

        public ICollection<CountBookInBasketViewModel> Books { get; set; }

        public int CountBooksInBasket { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal BasketTotalPrice { get; set; }

        public int CountBooksInFavorite { get; set; }

        public ICollection<AllCategoriesViewModel> YouMayAlsoLike { get; set; }
    }
}

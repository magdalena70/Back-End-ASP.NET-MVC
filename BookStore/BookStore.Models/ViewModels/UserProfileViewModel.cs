using BookStore.Models.EntityModels;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }

        public Basket Basket { get; set; }

        public ICollection<CountBookInBasketViewModel> Books { get; set; }

        public int CountBooksInBasket { get; set; }

        public decimal BasketTotalPrice { get; set; }

        public int CountBooksInFavorite { get; set; }

        public ICollection<AllCategoriesViewModel> YouMayAlsoLike { get; set; }
    }
}

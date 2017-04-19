using BookStore.Models.ViewModels.Book;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<BooksViewModel> Top3BooksFromLastYear { get; set; }

        public IEnumerable<BooksViewModel> Top3BooksFromThisYear { get; set; }

        public IEnumerable<HomePromotionViewModel> CurrentPromotions { get; set; }

        public IEnumerable<HomeNewBookViewModel> NewBooks { get; set; }

    }
}

using BookStore.Models.ViewModels.Book;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels.User
{
    public class UserFavoriteBooksViewModel
    {
        public string UserName { get; set; }

        public ICollection<BooksViewModel> FavoriteBooks { get; set; }
    }
}

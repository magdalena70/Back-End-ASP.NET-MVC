using BookStore.Models.EntityModels;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class UserFavoriteBooksViewModel
    {
        public string UserName { get; set; }

        public List<Book> FavoriteBooks { get; set; }
    }
}

using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class UserFavoriteBooksViewModel
    {
        public string UserName { get; set; }

        public ICollection<BooksViewModel> FavoriteBooks { get; set; }
    }
}

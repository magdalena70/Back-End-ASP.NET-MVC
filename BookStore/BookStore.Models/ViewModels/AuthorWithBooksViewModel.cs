using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class AuthorWithBooksViewModel
    {
        public string FullName { get; set; }

        public ICollection<BooksViewModel> Books { get; set; }

        public int BooksCount { get; set; }
    }
}

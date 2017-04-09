using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class AuthorWithBooksViewModel
    {
        public string FullName { get; set; }

        public List<BooksViewModel> Books { get; set; }

        public int BooksCount { get; set; }
    }
}

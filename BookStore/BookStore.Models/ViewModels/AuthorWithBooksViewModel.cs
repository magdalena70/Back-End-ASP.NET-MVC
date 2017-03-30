using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class AuthorWithBooksViewModel
    {
        public string FullName { get; set; }

        public List<Book> Books { get; set; }

        public int BooksCount { get; set; }
    }
}

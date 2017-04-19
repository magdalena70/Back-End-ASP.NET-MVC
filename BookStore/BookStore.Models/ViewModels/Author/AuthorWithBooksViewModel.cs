using BookStore.Models.ViewModels.Book;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels.Author
{
    public class AuthorWithBooksViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Bio { get; set; }

        public ICollection<BooksViewModel> Books { get; set; }

        public int BooksCount { get; set; }
    }
}

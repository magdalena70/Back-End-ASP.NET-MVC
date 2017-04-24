using BookStore.Models.ViewModels.Author;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels.Book
{
    public class AllBooksViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public ICollection<AuthorViewModel> Authors { get; set; }
    }
}

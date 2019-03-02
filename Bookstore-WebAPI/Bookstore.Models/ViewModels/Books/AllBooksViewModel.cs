using System.Collections.Generic;

namespace Bookstore.Models.ViewModels.Books
{
    public class AllBooksViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public ICollection<string> AuthorsFullName { get; set; }
    }
}

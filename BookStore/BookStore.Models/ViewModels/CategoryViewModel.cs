using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BooksViewModel> Books { get; set; }

        public int BooksCount { get; set; }
    }
}

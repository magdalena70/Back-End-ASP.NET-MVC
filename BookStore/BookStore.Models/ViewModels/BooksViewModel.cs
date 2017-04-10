using BookStore.Models.EntityModels;
using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class BooksViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<AuthorViewModel> Authors { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime IssueDate { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime IssueDate { get; set; }

        public string ISBN { get; set; }

        public int UpRating { get; set; }

        public int DownRating { get; set; }

        public virtual List<Category> Categories { get; set; }

        public virtual List<Author> Authors { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}

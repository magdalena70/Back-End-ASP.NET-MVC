using BookStore.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int NumberOfPages { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }

        public string ISBN { get; set; }

        public virtual List<Category> Categories { get; set; }

        public virtual List<AuthorViewModel> Authors { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<Purchase> Purchases { get; set; }

        public virtual List<Rating> Ratings { get; set; }


    }
}

using Bookstore.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bookstore.Models.ViewModels.Books
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

        [Display(Name = "In Stock")]
        public int Quantity { get; set; }

        [Display(Name = "Pages")]
        public int NumberOfPages { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }

        public string ISBN { get; set; }

        public List<Category> Categories { get; set; }

        public List<Author> Authors { get; set; }

        public List<Review> Reviews { get; set; }

        [Display(Name = "Purchases Count")]
        public int PurchasesCount { get; set; }

        public List<Rating> Ratings { get; set; }

        public bool IsCurrentUserRated { get; set; }

        public int CurrentUserRatingValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double AvgRating { get; set; }

        public ICollection<SelectListItem> SelectAuthors { get; set; }

        public ICollection<SelectListItem> SelectCategories { get; set; }
    }
}

using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Author;
using BookStore.Models.ViewModels.Category;
using BookStore.Models.ViewModels.Rating;
using BookStore.Models.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.Models.ViewModels.Book
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

        public List<CategoryViewModel> Categories { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public List<ReviewViewModel> Reviews { get; set; }

        [Display(Name = "Purchases Count")]
        public int PurchasesCount { get; set; }

        public List<RatingViewModel> Ratings { get; set; }

        public bool IsCurrentUserRated { get; set; }

        public int CurrentUserRatingValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double AvgRating { get; set; }

        public ICollection<SelectListItem> SelectAuthors { get; set; }

        public ICollection<SelectListItem> SelectCategories { get; set; }
    }
}

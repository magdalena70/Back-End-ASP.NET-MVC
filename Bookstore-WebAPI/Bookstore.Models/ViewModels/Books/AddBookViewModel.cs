using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bookstore.Models.ViewModels.Books
{
    public class AddBookViewModel
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required, MaxLength(10)]
        public string Language { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required, MaxLength(20)]
        public string ISBN { get; set; }

        public ICollection<SelectListItem> Authors { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
            this.Reviews = new HashSet<Review>();
            this.Categories = new HashSet<Category>();
            this.Baskets = new HashSet<BasketBook>();
            this.Fans = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        public DateTime IssueDate { get; set; }

        public string ISBN { get; set; }

        public int UpRating { get; set; }

        public int DownRating { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<BasketBook> Baskets { get; set; }

        public virtual ICollection<User> Fans { get; set; }
    }
}

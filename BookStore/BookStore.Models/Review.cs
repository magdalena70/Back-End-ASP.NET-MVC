using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime DateCreate { get; set; }

        public virtual User Author { get; set; }

        public virtual Book Book { get; set; }
    }
}

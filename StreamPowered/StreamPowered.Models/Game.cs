
namespace StreamPowered.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public Game()
        {
            this.Ratings = new HashSet<Rating>();
            this.Reviews = new HashSet<Review>();
            this.ImageUrls = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string SystemRequirements { get; set; }

        public decimal AverageRating { get; set; }

        public virtual User Author { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Image> ImageUrls { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}

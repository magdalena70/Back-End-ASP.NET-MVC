
namespace StreamPowered.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public virtual Game Game { get; set; }
    }
}

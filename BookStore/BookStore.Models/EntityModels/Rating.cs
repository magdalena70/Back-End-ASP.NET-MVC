using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.EntityModels
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public virtual Book Book { get; set; }
    }
}

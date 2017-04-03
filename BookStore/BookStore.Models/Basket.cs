using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Basket
    {
        public Basket()
        {
            this.Books = new HashSet<BasketBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<BasketBook> Books { get; set; }
    }
}

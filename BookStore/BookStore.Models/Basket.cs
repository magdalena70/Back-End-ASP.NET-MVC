using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Basket
    {
        public Basket()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

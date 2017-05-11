using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.EntityModels
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
            this.Promotions = new HashSet<Promotion>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}

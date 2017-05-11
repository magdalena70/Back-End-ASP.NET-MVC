using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.EntityModels
{
    public class Rating
    {
        public Rating()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Value { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.EntityModels
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Bio { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

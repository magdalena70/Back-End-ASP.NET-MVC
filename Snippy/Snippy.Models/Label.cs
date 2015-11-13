
namespace Snippy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Label
    {
        public Label()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(15)]
        public string Text { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}

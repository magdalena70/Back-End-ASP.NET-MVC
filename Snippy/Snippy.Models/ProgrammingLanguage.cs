
namespace Snippy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProgrammingLanguage
    {
        public ProgrammingLanguage()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}

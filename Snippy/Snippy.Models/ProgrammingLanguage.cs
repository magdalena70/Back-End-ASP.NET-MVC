
namespace Snippy.Models
{
    using System.Collections.Generic;

    public class ProgrammingLanguage
    {
        public ProgrammingLanguage()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}

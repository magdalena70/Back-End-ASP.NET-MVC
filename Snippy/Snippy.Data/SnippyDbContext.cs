
namespace Snippy.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Snippy.Models;
    using System.Data.Entity;

    public class SnippyDbContext : IdentityDbContext<User>
    {
        public SnippyDbContext()
            : base("SnippyConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Snippet> Snippets { get; set; }

        public virtual IDbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Label> Labels { get; set; }

        public static SnippyDbContext Create()
        {
            return new SnippyDbContext();
        }
    }
}

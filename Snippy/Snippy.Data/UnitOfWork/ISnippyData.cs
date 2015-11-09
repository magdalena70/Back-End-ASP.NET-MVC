
namespace Snippy.Data.UnitOfWork
{
    using Snippy.Data.Repositories;
    using Snippy.Models;

    public interface ISnippyData
    {
        IRepository<User> Users { get; }

        IRepository<Snippet> Snippets { get; }

        IRepository<ProgrammingLanguage> ProgrammingLanguages { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Label> Labels { get; }

        void SaveChanges();
    }
}

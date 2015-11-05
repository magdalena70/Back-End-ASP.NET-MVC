
namespace SportSystem.Data.UnitOfWork
{
    using Repositories;
    using Models;

    public interface ISportSystemData
    {
        IRepository<User> Users { get; }

        IRepository<Team> Teams { get; }

        IRepository<Player> Players { get; }

        IRepository<Match> Matches { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Bet> Bets { get; }

        IRepository<Vote> Votes { get; }

        int SaveChanges();
    }
}

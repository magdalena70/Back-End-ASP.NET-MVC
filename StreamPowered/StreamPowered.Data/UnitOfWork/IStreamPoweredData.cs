
namespace StreamPowered.Data.UnitOfWork
{
    using StreamPowered.Data.Repositories;
    using StreamPowered.Models;

    public interface IStreamPoweredData
    {
        IRepository<User> Users { get; }

        IRepository<Game> Games { get; }

        IRepository<Review> Reviews { get; }

        IRepository<Genre> Genres { get; }

        IRepository<Rating> Ratings { get; }

        IRepository<Image> ImageUrls { get; }

        void SaveChanges();
    }
}

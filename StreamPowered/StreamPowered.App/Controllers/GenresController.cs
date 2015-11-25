
namespace StreamPowered.App.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using System.Data.Entity;
    using StreamPowered.App.Models.ViewModels;
    using StreamPowered.Data.UnitOfWork;
    using StreamPowered.Models;
    using AutoMapper;

    public class GenresController : BaseController
    {
        public GenresController(IStreamPoweredData data)
            : base(data)
        {
        }

        // GET: Genres
        public ActionResult Index(int page = 1, int count = 8)
        {
            var genres = this.Data.Genres.All()
                .Include(g => g.Games);
            int genresCount = genres.Count();
            genres = genres
                .OrderByDescending(g => g.Games.Count)
                .ThenBy(g => g.Name)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (genresCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            var model = Mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return this.View(model);
        }

        public ActionResult GamesByGenre(int id, int page = 1, int count = 3)
        {
            var genre = this.Data.Genres.Find(id);
            int gamesCount = genre.Games.Count();
            var genreGames = genre.Games
                .OrderByDescending(g => g.AverageRating)
                .ThenBy(g => g.Reviews.Count)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (gamesCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            var model = new GamesByGenrePageViewModel()
            {
                GenreName = genre.Name,
                Games = Mapper.Map<IEnumerable<TopFiveGamesViewModel>>(genreGames)
            };

            return this.View(model);
        }

        public ActionResult SearchGenre()
        {
            return this.View();
        }
    }
}
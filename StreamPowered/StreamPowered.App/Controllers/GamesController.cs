
namespace StreamPowered.App.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using System.Web.Mvc;
    using System.Linq;
    using StreamPowered.App.Models.ViewModels;
    using System.Collections.Generic;
    using System.Data.Entity;
    using AutoMapper;

    public class GamesController : BaseController
    {
        public GamesController(IStreamPoweredData data)
            : base(data)
        {
        }

        // GET: Games
        public ActionResult Index(int page = 1, int count = 3)
        {
            var games = this.Data.Games.All()
                .Include(g => g.ImageUrls);
            int gamesCount = games.Count();
            games = games
                .OrderByDescending(g => g.AverageRating)
                .ThenBy(g => g.Title)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (gamesCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            var model = Mapper.Map<IEnumerable<TopFiveGamesViewModel>>(games);

            return this.View(model);
        }

        public ActionResult GameDetails(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<GameDetailsViewModel>(game);
            return this.View(model);
        }
    }
}
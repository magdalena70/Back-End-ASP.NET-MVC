
namespace StreamPowered.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;
    using StreamPowered.App.Models.ViewModels;
    using StreamPowered.Data.UnitOfWork;
    using StreamPowered.Models;
    using AutoMapper;

    public class GamesController : BaseAdminController
    {
        public GamesController(IStreamPoweredData data)
            : base(data)
        {
        }

        // GET: Admin/Games
        public ActionResult Index(int page = 1, int count = 5)
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

            return View(model);
        }

        // GET: Admin/Games/Details/5
        public ActionResult Details(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<GameDetailsViewModel>(game);

            return this.View(model);
        }

        // GET: Admin/Games/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Admin/Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,SystemRequirements,AverageRating")] Game game)
        {
            if (ModelState.IsValid)
            {
                this.Data.Games.Add(game);
                this.Data.SaveChanges();
                return RedirectToAction("Details", "Games", new { id = game.Id});
            }

            return this.View(game);
        }

        // GET: Admin/Games/Edit/5
        public ActionResult Edit(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            return this.View(game);
        }

        // POST: Admin/Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,SystemRequirements,Author,Genre,AverageRating")] Game game)
        {
            if (ModelState.IsValid)
            {
                this.Data.Games.Update(game);
                this.Data.SaveChanges();
                return RedirectToAction("Details", "Games", new { id = game.Id});
            }

            return View(game);
        }

        // GET: Admin/Games/Delete/5
        public ActionResult Delete(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            return this.View(game);
        }

        // POST: Admin/Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            this.Data.Games.Remove(game);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

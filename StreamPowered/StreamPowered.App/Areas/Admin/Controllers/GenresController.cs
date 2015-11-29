
namespace StreamPowered.App.Areas.Admin.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using StreamPowered.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;

    public class GenresController : BaseAdminController
    {
        public GenresController(IStreamPoweredData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGenre(Genre genreModel, int id)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            if (genreModel != null)
            {
                var genre = new Genre();
                if(this.Data.Genres.All().Any(g => g.Name.ToUpper() == genreModel.Name.ToUpper()))
                {
                    genre = this.Data.Genres.All().FirstOrDefault(g => g.Name == genreModel.Name);
                    game.Genre = genre;
                }
                else
                {
                    genre = new Genre()
                    {
                        Name = genreModel.Name,
                        Games = new List<Game>()
                        {
                            game
                        }
                    };

                    this.Data.Genres.Add(genre);
                }
                
                this.Data.SaveChanges();

                var genreDb = this.Data.Genres.All()
                    .FirstOrDefault(c => c.Id == genre.Id);
                
                return this.Content(genre.Name);
            }

            return this.Json("Error");
        }

        public ActionResult SelectGenre()
        {
            var allGenres = this.Data.Genres.All();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var genre in allGenres)
            {
                items.Add(new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() });
            }

            ViewBag.Genres = items;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectGenre(int id, string Genres)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            game.Genre = this.Data.Genres.Find(int.Parse(Genres));
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Games", new { id = game.Id });
        }
    }
}
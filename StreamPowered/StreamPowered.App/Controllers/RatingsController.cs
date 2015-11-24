using System;

namespace StreamPowered.App.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using System.Web.Mvc;
    using System.Linq;
    using StreamPowered.App.Models.ViewModels;
    using System.Collections.Generic;
    using AutoMapper;
    using StreamPowered.Models;

    public class RatingsController : BaseController
    {
        public RatingsController(IStreamPoweredData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddOne(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game.Author.Id != this.UserProfile.Id)
            {
                this.Data.Ratings.Add(new Rating()
                {
                    Author = this.Data.Users.All().FirstOrDefault(u => u.Id == this.UserProfile.Id),
                    Game = this.Data.Games.All().FirstOrDefault(g => g.Id == id),
                    Value = 1
                });

                this.Data.SaveChanges();
            }

            var ratings = this.Data.Ratings.All().Where(r => r.Game.Id == id);
            var totalCount = ratings.Count();
            var count = 0;
            foreach(var rating in ratings)
            {
                if (rating.Value == 1)
                {
                    count += 1;
                }
            }

            return this.Content(
                string.Format(
                "<span class='badge'>Rated {0} time(s)</span><br/>Rated {1} time(s) with value <span style='color: red;'>1</span>",
                totalCount,
                count
                ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddTwo(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game.Author.Id != this.UserProfile.Id)
            {
                this.Data.Ratings.Add(new Rating()
                {
                    Author = this.Data.Users.All().FirstOrDefault(u => u.Id == this.UserProfile.Id),
                    Game = this.Data.Games.All().FirstOrDefault(g => g.Id == id),
                    Value = 2
                });

                this.Data.SaveChanges();
            }

            var ratings = this.Data.Ratings.All().Where(r => r.Game.Id == id);
            var totalCount = ratings.Count();
            var count = 0;
            foreach (var rating in ratings)
            {
                if (rating.Value == 2)
                {
                    count += 1;
                }
            }

            return this.Content(
                string.Format(
                "<span class='badge'>Rated {0} time(s)</span><br/>Rated {1} time(s) with value <span style='color: red;'>2</span>",
                totalCount,
                count
                ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddThree(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game.Author.Id != this.UserProfile.Id)
            {
                this.Data.Ratings.Add(new Rating()
                {
                    Author = this.Data.Users.All().FirstOrDefault(u => u.Id == this.UserProfile.Id),
                    Game = this.Data.Games.All().FirstOrDefault(g => g.Id == id),
                    Value = 3
                });

                this.Data.SaveChanges();
            }

            var ratings = this.Data.Ratings.All().Where(r => r.Game.Id == id);
            var totalCount = ratings.Count();
            var count = 0;
            foreach (var rating in ratings)
            {
                if (rating.Value == 3)
                {
                    count += 1;
                }
            }

            return this.Content(
                string.Format(
                "<span class='badge'>Rated {0} time(s)</span><br/>Rated {1} time(s) with value <span style='color: red;'>3</span>",
                totalCount,
                count
                ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddFour(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game.Author.Id != this.UserProfile.Id)
            {
                this.Data.Ratings.Add(new Rating()
                {
                    Author = this.Data.Users.All().FirstOrDefault(u => u.Id == this.UserProfile.Id),
                    Game = this.Data.Games.All().FirstOrDefault(g => g.Id == id),
                    Value = 4
                });

                this.Data.SaveChanges();
            }

            var ratings = this.Data.Ratings.All().Where(r => r.Game.Id == id);
            var totalCount = ratings.Count();
            var count = 0;
            foreach (var rating in ratings)
            {
                if (rating.Value == 4)
                {
                    count += 1;
                }
            }

            return this.Content(
                string.Format(
                "<span class='badge'>Rated {0} time(s)</span><br/>Rated {1} time(s) with value <span style='color: red;'>4</span>",
                totalCount,
                count
                ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddFive(int id)
        {
            var game = this.Data.Games.Find(id);
            if (game.Author.Id != this.UserProfile.Id)
            {
                this.Data.Ratings.Add(new Rating()
                {
                    Author = this.Data.Users.All().FirstOrDefault(u => u.Id == this.UserProfile.Id),
                    Game = this.Data.Games.All().FirstOrDefault(g => g.Id == id),
                    Value = 5
                });

                this.Data.SaveChanges();
            }

            var ratings = this.Data.Ratings.All().Where(r => r.Game.Id == id);
            var totalCount = ratings.Count();
            var count = 0;
            foreach (var rating in ratings)
            {
                if (rating.Value == 5)
                {
                    count += 1;
                }
            }

            return this.Content(
                string.Format(
                "<span class='badge'>Rated {0} time(s)</span><br/>Rated {1} time(s) with value <span style='color: red;'>5</span>",
                totalCount,
                count
                ));
        }
    }
}
using System;

namespace StreamPowered.App.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using StreamPowered.Models;
    using StreamPowered.App.Models.ViewModels;
    using AutoMapper;

    public class RatingsController : BaseController
    {
        public RatingsController(IStreamPoweredData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddRating(int id, int value)
        {
            var game = this.Data.Games.Find(id);
            if (game.Author.Id != this.UserProfile.Id)
            {
                this.Data.Ratings.Add(new Rating()
                {
                    Author = this.Data.Users.All().FirstOrDefault(u => u.Id == this.UserProfile.Id),
                    Game = this.Data.Games.All().FirstOrDefault(g => g.Id == id),
                    Value = value
                });

                CalculateAverageRating(game);
                this.Data.SaveChanges();
            }
            else
            {
                return this.Content(
                    string.Format(
                    "<span style='color: red;'>Can not rated your own game!</span>"
                    ));
            }

            var ratings = this.Data.Ratings.All().Where(r => r.Game.Id == id);
            var totalCount = ratings.Count();
            var count = 0;
            foreach(var rating in ratings)
            {
                if (rating.Value == value)
                {
                    count += 1;
                }
            }

            return this.Content(
                string.Format(
                "<span class='badge'>Rated {0} time(s)</span><br/>Rated {1} time(s) with value <span style='color: red;'>{2}</span>",
                totalCount,
                count, value
                ));
        }

        private void CalculateAverageRating(Game game)
        {
            decimal sumRatingValue = 0;
            decimal count = 0;
            decimal avgRating = 0;
            if (game.Ratings.Any())
            {
                count = game.Ratings.Count();
                foreach (var rating in game.Ratings)
                {
                    sumRatingValue += rating.Value;
                }

                avgRating = sumRatingValue / count;
                game.AverageRating = avgRating;
            }

            this.Data.SaveChanges();
        }
    }
}
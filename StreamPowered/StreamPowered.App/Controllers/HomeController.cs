
namespace StreamPowered.App.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using System.Web.Mvc;
    using System.Linq;
    using StreamPowered.App.Models.ViewModels;
    using System.Collections.Generic;
    using System.Data.Entity;
    using AutoMapper;
    using StreamPowered.Models;

    public class HomeController : BaseController
    {
        public HomeController(IStreamPoweredData data)
            : base(data)
        {
        }
      
        public ActionResult Index()
        {
            CalculateAverageRating();

            var topFiveGames = this.Data.Games.All()
                .Include(g => g.Ratings)
                .Include(g => g.ImageUrls)
                .OrderByDescending(g => g.AverageRating)
                .ThenBy(g => g.Title)
                .Take(5);

            var topFiveReviews = this.Data.Reviews.All()
                .OrderByDescending(r => r.CreationTime)
                .ThenByDescending(r => r.Id)
                .Take(5);

            var model = new HomePageViewModel()
            {
                TopFiveGames = Mapper.Map<IEnumerable<TopFiveGamesViewModel>>(topFiveGames),
                TopFiveReviews = Mapper.Map<IEnumerable<TopFiveReviewsViewModel>>(topFiveReviews),
            };

            return View(model);
        }

        private void CalculateAverageRating()
        {
            var games = this.Data.Games.All()
                .Include(g => g.Ratings);

            foreach (var game in games)
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
            }

            this.Data.SaveChanges();
        }
    }
}
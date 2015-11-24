
namespace StreamPowered.App.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using System.Web.Mvc;
    using System.Linq;
    using StreamPowered.App.Models.ViewModels;
    using System.Collections.Generic;
    using AutoMapper;
    using StreamPowered.Models;
    using System;
    public class ReviewsController : BaseController
    {
        public ReviewsController(IStreamPoweredData data)
            : base(data)
        {
        }

        // GET: Reviews
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(Review commentModel, int id)
        {
            if (commentModel != null)
            {
                var review = new Review()
                {
                    CreationTime = DateTime.Now,
                    Content = commentModel.Content,
                    Author = this.Data.Users.All().FirstOrDefault(u => u.UserName == this.UserProfile.UserName),
                    Game = this.Data.Games.Find(id)
                };

                this.Data.Reviews.Add(review);
                this.Data.SaveChanges();

                var reviewDb = this.Data.Reviews.All()
                    .FirstOrDefault(c => c.Id == review.Id);
                var model = Mapper.Map<Review, TopFiveReviewsViewModel>(reviewDb);

                return this.PartialView("DisplayTemplates/TopFiveReviewsViewModel", model);
            }

            return this.Json("Error");
        }

        public ActionResult EditReview(int id)
        {
            return this.View();
        }

        public ActionResult ReviewDetailsToDelete(int id)
        {
            var review = this.Data.Reviews.Find(id);
            var model = Mapper.Map<Review, ReviewDetailsViewModel>(review);

            return this.View(model);
        }

        public ActionResult DeleteReview(int id)
        {
            var review = this.Data.Reviews.Find(id);
            var gameId = review.Game.Id;
            this.Data.Reviews.Remove(review);
            this.Data.SaveChanges();

            return this.RedirectToAction("GameDetails", "Games", new { id = gameId });
        }
    }
}
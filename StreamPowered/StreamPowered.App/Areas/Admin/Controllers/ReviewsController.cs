
namespace StreamPowered.App.Areas.Admin.Controllers
{
    using StreamPowered.Data.UnitOfWork;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using AutoMapper;
    using System.Collections.Generic;
    using StreamPowered.App.Models.ViewModels;
    using StreamPowered.Models;

    public class ReviewsController : BaseAdminController
    {
        public ReviewsController(IStreamPoweredData data)
            : base(data)
        {
        }

        // GET: Admin/Reviews
        public ActionResult Index(int page = 1, int count = 5)
        {
            var reviews = this.Data.Reviews.All()
                .Include(r => r.Game);

            int reviewsCount = reviews.Count();
            reviews = reviews
                .OrderBy(r => r.Game.Title)
                .ThenBy(r => r.CreationTime)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (reviewsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            var model = Mapper.Map<IEnumerable<TopFiveReviewsViewModel>>(reviews);
            return View(model);
        }

        // GET: Admin/Reviews/Details/5
        public ActionResult Details(int id)
        {
            var review = this.Data.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<TopFiveReviewsViewModel>(review);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Review reviewModel)
        {
            var review = this.Data.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            if (reviewModel.Content != null)
            {
                review.Content = reviewModel.Content;
                review.Author = review.Author;
                review.Game = review.Game;
                this.Data.SaveChanges();

                var reviewDb = this.Data.Reviews.Find(review.Id);
                return this.Content(reviewDb.Content);
            }

            return this.Content("<h3 class='text-danger'>Error! Content can not be empty.</h3>");
        }

        // GET: Admin/Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            var review = this.Data.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: Admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var review = this.Data.Reviews.Find(id);
            this.Data.Reviews.Remove(review);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

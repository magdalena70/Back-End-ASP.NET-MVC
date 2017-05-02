using System.Web.Mvc;
using BookStore.Services;
using BookStore.Models.BindingModels.Review;
using BookStore.Models.ViewModels.Review;
using Microsoft.AspNet.Identity;

namespace BookStore.App.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private ReviewService reviewService;

        public ReviewsController()
        {
            this.reviewService = new ReviewService();
        }

        //POST Books/Details/5
        [HttpPost, ActionName("AddReview")]
        [ValidateInput(false)]
        public ActionResult AddReview(AddReviewBindingModel bindingModel, int id)
        {
            if (bindingModel != null)
            {
                string authorId = User.Identity.GetUserId();
                ReviewViewModel viewModel = this.reviewService.AddReviewAndGetResult(bindingModel, id, authorId);
                
                return this.PartialView("DisplayTemplates/ReviewViewModel", viewModel);
            }

            return Json("Error");
        }

        //POST Books/Details/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(int id)
        {
            this.reviewService.DeleteReview(id);
            return null;
        }
    }
}

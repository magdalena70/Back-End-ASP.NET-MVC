using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BookStore.Services;
using BookStore.Models.BindingModels.Rating;

namespace BookStore.App.Controllers
{
    [Authorize]
    public class RatingsController : Controller
    {
        private RatingService ratingService;

        public RatingsController()
        {
            this.ratingService = new RatingService();
        }

        //POST Books/Details/5
        [HttpPost, ActionName("AddRating")]
        [ValidateAntiForgeryToken]
        public ActionResult AddRating(int id, AddRatingBindingModel bindingModel)
        {
            if (bindingModel != null)
            {
                string userId = User.Identity.GetUserId();
                this.ratingService.AddRating(id, bindingModel, userId);
                
                return Json($"You rated with {bindingModel.Value}");
            }

            return Json("Error");
        }
    }
}

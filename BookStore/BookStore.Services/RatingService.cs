using BookStore.Models.BindingModels.Rating;
using BookStore.Models.EntityModels;
using AutoMapper;

namespace BookStore.Services
{
    public class RatingService : Service
    {
        public void AddRating(int id, AddRatingBindingModel bindingModel, string userId)
        {
            Rating newRating = Mapper.Map<AddRatingBindingModel, Rating>(bindingModel);
            newRating.UserId = userId;
            Book currentBook = this.Context.Books.Find(id);
            newRating.Books.Add(currentBook);
          
            this.Context.Ratings.Add(newRating);
            this.Context.SaveChanges();
        }
    }
}

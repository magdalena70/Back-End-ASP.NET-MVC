using BookStore.Models.BindingModels.Rating;

namespace BookStore.Services.Interfaces
{
    public interface IRatingService
    {
        void AddRating(int id, AddRatingBindingModel bindingModel, string userId);
    }
}
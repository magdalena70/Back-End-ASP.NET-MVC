using BookStore.Models.BindingModels.Review;
using BookStore.Models.ViewModels.Review;

namespace BookStore.Services.Interfaces
{
    public interface IReviewService
    {
        ReviewViewModel AddReviewAndGetResult(AddReviewBindingModel bindingModel, int bookId, string authorId);
        void DeleteReview(int id);
    }
}
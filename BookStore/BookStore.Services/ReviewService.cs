using System;
using BookStore.Models.BindingModels.Review;
using BookStore.Models.EntityModels;
using System.Web;
using AutoMapper;
using BookStore.Models.ViewModels.Review;

namespace BookStore.Services
{
    public class ReviewService : Service
    {
        public ReviewViewModel AddReviewAndGetResult(AddReviewBindingModel bindingModel, int bookId, string authorId)
        {
            var newReview = new Review()
            {
                Author = this.Context.Users.Find(authorId),
                Book = this.Context.Books.Find(bookId),
                DateCreate = DateTime.Now,
                Text = HttpUtility.HtmlDecode(bindingModel.Text)
            };
            this.Context.Reviews.Add(newReview);
            this.Context.SaveChanges();

            var newReviewDb = this.Context.Reviews.Find(newReview.Id);
            var viewModel = Mapper.Map<Review, ReviewViewModel>(newReviewDb);
            return viewModel;
        }

        public void DeleteReview(int id)
        {
            Review review = this.Context.Reviews.Find(id);
            int bookId = review.Book.Id;

            this.Context.Reviews.Remove(review);
            this.Context.SaveChanges();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Review
{
    public class AddReviewBindingModel
    {
        [Required, MaxLength(200)]
        public string Text { get; set; }
    }
}

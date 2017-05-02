using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Rating
{
    public class AddRatingBindingModel
    {
        [Required]
        public int Value { get; set; }
    }
}

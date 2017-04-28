using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Book
{
    public class AddCategoryToBookBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string SelectCategories { get; set; }
    }
}

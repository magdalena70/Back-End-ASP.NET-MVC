using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.BindingModels.Books
{
    public class AddCategoryToBookBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string SelectCategories { get; set; }
    }
}

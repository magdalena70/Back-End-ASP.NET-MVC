using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Category
{
    public class AddCategoryBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Category
{
    public class EditCategoryBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

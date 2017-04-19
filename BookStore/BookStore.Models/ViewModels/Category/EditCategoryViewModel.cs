using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Category
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels
{
    public class CategoryBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

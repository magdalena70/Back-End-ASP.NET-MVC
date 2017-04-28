using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Book
{
    public class AddAuthorToBookBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string SelectAuthors { get; set; }
    }
}

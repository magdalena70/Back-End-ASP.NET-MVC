using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.BindingModels.Books
{
    public class AddAuthorToBookBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string SelectAuthors { get; set; }
    }
}

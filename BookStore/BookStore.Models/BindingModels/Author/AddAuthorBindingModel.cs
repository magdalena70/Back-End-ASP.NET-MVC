using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.BindingModels.Author
{
    public class AddAuthorBindingModel
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Bio { get; set; }
    }
}

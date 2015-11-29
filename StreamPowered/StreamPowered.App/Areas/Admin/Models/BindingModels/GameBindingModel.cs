
namespace StreamPowered.App.Models.BindingModels
{
    using StreamPowered.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.WebPages.Html;

    public class GameBindingModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string SystemRequirements { get; set; }

        public decimal AverageRating { get; set; }

        public IEnumerable<SelectListItem> Author { get; set; }

        [Required]
        public IEnumerable<SelectListItem> Genre { get; set; }
    }
}
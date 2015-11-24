
namespace StreamPowered.App.Models.ViewModels
{
    using StreamPowered.Models;
    using System.Collections.Generic;

    public class GameDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SystemRequirements { get; set; }

        public string Genre { get; set; }

        public int GenreId { get; set; }

        public string Author { get; set; }

        public IEnumerable<Image> ImageUrls { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }

        public IEnumerable<TopFiveReviewsViewModel> Reviews { get; set; }
    }
}
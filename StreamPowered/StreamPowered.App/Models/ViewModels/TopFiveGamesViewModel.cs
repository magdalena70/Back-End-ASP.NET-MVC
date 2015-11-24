
namespace StreamPowered.App.Models.ViewModels
{
    using StreamPowered.Models;
    using System;
    using System.Collections.Generic;

    public class TopFiveGamesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RatingCount { get; set; }

        public decimal AverageRating { get; set; }

        public IEnumerable<Image> ImageUrls { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }
    }
}
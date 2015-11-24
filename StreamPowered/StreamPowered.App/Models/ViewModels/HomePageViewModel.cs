
namespace StreamPowered.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<TopFiveGamesViewModel> TopFiveGames { get; set; }

        public IEnumerable<TopFiveReviewsViewModel> TopFiveReviews { get; set; }
    }
}
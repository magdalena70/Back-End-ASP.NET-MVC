
namespace StreamPowered.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class GamesByGenrePageViewModel
    {
        public string GenreName { get; set; }

        public IEnumerable<TopFiveGamesViewModel> Games { get; set; }
    }
}
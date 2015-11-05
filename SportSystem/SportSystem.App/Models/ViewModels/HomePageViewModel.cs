namespace SportSystem.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<ConciseMatchViewModel> TopMatches { get; set; }

        public IEnumerable<ConciseTeamViewModel> BestTeams { get; set; }
    }
}
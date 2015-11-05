namespace SportSystem.App.Models.ViewModels
{
    using System;

    public class ConciseMatchViewModel
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime DateAndTime { get; set; }
    }
}
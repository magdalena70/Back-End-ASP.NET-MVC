
namespace StreamPowered.App.Models.ViewModels
{
    using System;

    public class ReviewDetailsViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public int GameId { get; set; }

        public string GameTitle { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
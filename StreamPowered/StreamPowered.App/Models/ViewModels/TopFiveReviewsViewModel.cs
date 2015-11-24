using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamPowered.App.Models.ViewModels
{
    public class TopFiveReviewsViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public string Author { get; set; }

        public string GameTitle { get; set; }

        public int GameId { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Home
{
    public class HomeNewBookViewModel
    {
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<HomeNewBookAuthorViewModel> Authors { get; set; }
    }
}

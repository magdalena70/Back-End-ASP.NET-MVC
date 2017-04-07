using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class PromotionsViewModel
    {
        public string Category { get; set; }

        public decimal Discount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<PromotionBookViewModel> Books { get; set; }
    }
}

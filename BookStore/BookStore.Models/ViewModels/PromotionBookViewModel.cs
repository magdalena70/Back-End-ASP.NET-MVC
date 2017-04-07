using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class PromotionBookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public List<Author> Authors { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal NewPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime IssueDate { get; set; }
    }
}

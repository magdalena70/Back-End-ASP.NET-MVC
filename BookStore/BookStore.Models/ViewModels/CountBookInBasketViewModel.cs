using BookStore.Models.EntityModels;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class CountBookInBasketViewModel
    {
        public int BookId { get; set; }

        public int Count { get; set; }

        public int NewCount { get; set; }

        public Book Book { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal PromotionDiscount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal NewPrice { get; set; }
    }
}

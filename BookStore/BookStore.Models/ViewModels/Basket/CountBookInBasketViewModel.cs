using BookStore.Models.ViewModels.Book;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Basket
{
    public class CountBookInBasketViewModel
    {
        public int BookId { get; set; }

        public int Count { get; set; }

        public int NewCount { get; set; }

        public BookDetailsViewModel Book { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal PromotionDiscount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal NewPrice { get; set; }
    }
}

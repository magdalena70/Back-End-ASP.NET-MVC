using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class HomePromotionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }
    }
}

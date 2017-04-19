using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Promotion
{
    public class PromotionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }
    }
}

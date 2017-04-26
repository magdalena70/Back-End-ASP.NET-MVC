using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Purchase
{
    public class AllPurchasesViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CompletedOndate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }
    }
}

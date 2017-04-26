using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Purchase
{
    public class EditPurchaseViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CompletedOndate { get; set; }

        public bool IsCompleted { get; set; }

        public string DeliveryAddress { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal DeliveryPrice { get; set; }
    }
}

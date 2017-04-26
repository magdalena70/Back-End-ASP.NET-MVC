using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Purchase
{
    public class EditPurchaseBindingModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CompletedOndate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal DeliveryPrice { get; set; }
    }
}

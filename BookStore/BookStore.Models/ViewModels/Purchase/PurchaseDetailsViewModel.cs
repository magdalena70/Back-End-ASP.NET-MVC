using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Purchase
{
    public class PurchaseDetailsViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime CompletedOndate { get; set; }

        public bool IsCompleted { get; set; }

        public string DeliveryAddress { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal DeliveryPrice { get; set; }

        public UserPurchaseViewModel User { get; set; }

        public ICollection<CountBookInBasketViewModel> Books { get; set; }

        [Display(Name = "Quantity")]
        public int BooksCount { get; set; }
    }
}

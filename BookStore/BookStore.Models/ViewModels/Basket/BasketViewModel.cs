using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Basket
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string OwnerUserName { get; set; }

        public string OwnerAddress { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerPhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal YouWillSave { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal LastPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} BGN", ApplyFormatInEditMode = true)]
        public decimal DeliveryPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        public IEnumerable<CountBookInBasketViewModel> Books { get; set; }
    }
}

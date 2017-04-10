using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string OwnerUserName { get; set; }

        public string OwnerAddress { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerPhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal ShippingPrice { get; set; }

        public IEnumerable<CountBookInBasketViewModel> Count { get; set; }
    }
}

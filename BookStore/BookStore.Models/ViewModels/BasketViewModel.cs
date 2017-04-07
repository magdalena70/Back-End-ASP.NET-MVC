using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string OwnerUserName { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<CountBookInBasketViewModel> Count { get; set; }

        public User User { get; set; }
    }
}

using System;
using BookStore.Data;
using BookStore.Models.ViewModels;
using System.Linq;
using BookStore.Models;

namespace BookStore.Services
{
    public class BasketService : Service
    {
        public BasketService(BookStoreContext context) : base(context)
        {
        }

        public BasketViewModel GetBasketDetails(string ownerId)
        {
            Basket basket = context.Baskets
                .FirstOrDefault(b => b.Owner.Id == ownerId);
            if (basket == null)
            {
                return null;
            }

            BasketViewModel viewModel = new BasketViewModel()
            {
                Id = basket.Id,
                Discount = basket.Discount,
                TotalPrice = basket.TotalPrice,
                OwnerUserName = basket.Owner.UserName,
                Books = basket.Books.ToList()
            };

            return viewModel;
        }
    }
}

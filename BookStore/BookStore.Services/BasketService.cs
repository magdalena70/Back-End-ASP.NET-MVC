using System;
using BookStore.Data;
using BookStore.Models.ViewModels;
using System.Linq;
using BookStore.Models;
using BookStore.Models.BindingModels;
using System.Collections.Generic;

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
                Count = basket.Books
                    .GroupBy(b => b.Book.Id)
                    .Select(b => new CountBookInBasketViewModel()
                    {
                        Count = b.Count(),
                        Book = b.First().Book,
                        BookId = b.First().Book.Id
                    }).ToList()
            };

            return viewModel;
        }

        public void AddBookToBasket(User currUser, Book currBook)
        {
            Basket currBasket = currUser.Basket;
            if (currBasket == null)
            {
                Basket basket = new Basket()
                {
                    Owner = currUser,
                    TotalPrice = currBook.Price,
                    Discount = this.CheckDiscount(currBook.Price)
                };
                
                    this.context.BasketsBooks.Add(new BasketBook()
                    {
                        Basket = basket,
                        Book = currBook
                    });
            }
            else
            {
                 currBasket.TotalPrice += currBook.Price;
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice);
                    var newBook = new BasketBook()
                    {
                        Basket = currBasket,
                        Book = currBook
                    };
                    
                    this.context.BasketsBooks.Add(newBook);
            }
            if (currBook.Quantity > 0)
            {
                currBook.Quantity--;
            }
            
            context.SaveChanges();
        }

        public void RemoveBookFromBasket(Book currentBook, User currUser)
        {
            Basket currBasket = currUser.Basket;
            currBasket.TotalPrice -= currentBook.Price;
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice);
            var currBasketBooks = context.BasketsBooks
                .FirstOrDefault(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
            currBasket.Books.Remove(currBasketBooks);

            currentBook.Quantity++;
            context.SaveChanges();
        }

        private decimal CheckDiscount(decimal price)
        {
            var discount = 0m;
            if (price > 50)
            {
                discount = 5.0m;
            }

            if (price > 100)
            {
                discount = 10.0m;
            }

            if (price > 200)
            {
                discount = 15.0m;
            }

            return discount;
        }
    }
}

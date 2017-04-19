using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.Book;

namespace BookStore.Services
{
    public class BasketService : Service
    {

        public BasketViewModel GetBasketDetails(string ownerId)
        {
            Basket basket = this.Context.Baskets
                .FirstOrDefault(b => b.Owner.Id == ownerId);
            if (basket == null)
            {
                return null;
            }

            var countBooks = basket.Books
                    .GroupBy(b => b.Book.Id)
                    .Select(b => new CountBookInBasketViewModel()
                    {
                        Count = b.Count(),
                        Book = Mapper.Map<Book, BookDetailsViewModel>(b.First().Book),
                        BookId = b.First().Book.Id,
                        NewCount = 0
                    }).ToList();

            BasketViewModel viewModel = Mapper.Map<Basket, BasketViewModel>(basket);

            foreach (var countBook in countBooks)
            {
                this.CheckForCurrentPromotion(countBook);               
                countBook.NewPrice = this.CheckNewPrice(countBook.Book.Price, countBook.PromotionDiscount);
            }
            viewModel.Books = countBooks;
            viewModel.DeliveryPrice = this.CheckDeliveryPrice(basket.TotalPrice);

            return viewModel;
        }

        private void CheckForCurrentPromotion(CountBookInBasketViewModel countBook)
        {
            foreach (var category in countBook.Book.Categories)
            {
                foreach (var promotion in category.Promotions)
                {
                    if (promotion.StartDate <= DateTime.Now && promotion.EndDate > DateTime.Now)
                    {
                        countBook.PromotionDiscount = promotion.Discount;
                    }
                }
            }
        }

        private decimal CheckNewPrice(decimal price, decimal promotionDiscount)
        {
            decimal newPrice = price;
            if (promotionDiscount > 0)
            {
                newPrice = price - (price * promotionDiscount) / 100;
            }

            return newPrice;
        }

        public User GetCurrentUser(string authenticatedUserId)
        {
            User currentUser = this.Context.Users.Find(authenticatedUserId);
            return currentUser;
        }

        public Book GetCurrentBook(int bookId)
        {
            Book currentBook = this.Context.Books.Find(bookId);
            return currentBook;
        }

        public void AddBookToBasket(User currUser, Book currBook)
        {
            Basket currBasket = currUser.Basket;
            if (currBasket == null)
            {
                Basket basket = new Basket()
                {
                    Owner = currUser,
                    TotalPrice = this.CheckCurrentBookPrice(currBook),
                    Discount = this.CheckDiscount(currBook.Price, currUser.MoneySpentBalance)
                };

                this.Context.BasketsBooks.Add(new BasketBook()
                {
                    Basket = basket,
                    Book = currBook
                });
            }
            else
            {
                currBasket.TotalPrice += this.CheckCurrentBookPrice(currBook);
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
                var newBook = new BasketBook()
                {
                    Basket = currBasket,
                    Book = currBook
                };

                this.Context.BasketsBooks.Add(newBook);
            }
            if (currBook.Quantity > 0)
            {
                currBook.Quantity--;
            }

            this.Context.SaveChanges();
        }

        private decimal CheckCurrentBookPrice(Book currBook)
        {
            decimal currBookPrice = currBook.Price;
            foreach (var category in currBook.Categories)
            {
                foreach (var promotion in category.Promotions)
                {
                    if (promotion.StartDate <= DateTime.Now && promotion.EndDate > DateTime.Now)
                    {
                        currBookPrice = currBook.Price - (currBook.Price * promotion.Discount) / 100;
                    }
                }
            }

            return currBookPrice;
        }

        public void RemoveOneOfThisFromBasket(Book currentBook, User currUser)
        {
            Basket currBasket = currUser.Basket;
            currBasket.TotalPrice -= this.CheckCurrentBookPrice(currentBook);
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            var currBasketBooks = this.Context.BasketsBooks
                .FirstOrDefault(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
            currBasket.Books.Remove(currBasketBooks);

            currentBook.Quantity++;
            this.Context.SaveChanges();
        }

        public void RemoveAllOfThisFromBasket(Book currentBook, User currUser, int count)
        {
            Basket currBasket = currUser.Basket;
            currBasket.TotalPrice -= this.CheckCurrentBookPrice(currentBook) * count;
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            var currBasketBooks = this.Context.BasketsBooks
                .Where(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
            this.Context.BasketsBooks.RemoveRange(currBasketBooks);

            currentBook.Quantity += count;
            this.Context.SaveChanges();
        }

        private decimal CheckDiscount(decimal price, decimal moneySpentBalance)
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

            if (moneySpentBalance > 50)
            {
                discount += 2.0m;
            }

            if (moneySpentBalance > 150)
            {
                discount += 6.0m;
            }

            if (moneySpentBalance > 300)
            {
                discount += 10.0m;
            }

            return discount;
        }

        public void EditBookQuantityInBasket(Book currentBook, User currUser, int currQty, int newCount)
        {
            Basket currBasket = currUser.Basket;
      
            BasketBook bookInBasket = new BasketBook()
            {
                Basket = currBasket,
                Book = currentBook
            };

            int difference = 0;
            if (newCount > currQty)
            {
                difference = newCount - currQty;

                for (int i = 0; i < difference; i++)
                {
                    this.Context.BasketsBooks.Add(bookInBasket);
                    this.Context.SaveChanges();
                }

                currentBook.Quantity -= difference;
                currBasket.TotalPrice += this.CheckCurrentBookPrice(currentBook) * difference;
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            }

            if (newCount < currQty)
            {
                difference = currQty - newCount;
                //var editedBooks = Enumerable.Repeat(bookInBasket, newCount).ToList();
                var currBasketBooks = this.Context.BasketsBooks
                    .Where(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
                this.Context.BasketsBooks.RemoveRange(currBasketBooks);
                for (int i = 0; i < newCount; i++)
                {
                    this.Context.BasketsBooks.Add(bookInBasket);
                    this.Context.SaveChanges();
                }
              
                currBasket.TotalPrice -= this.CheckCurrentBookPrice(currentBook) * difference;
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
                currentBook.Quantity += difference;           
            }

            this.Context.SaveChanges();
        }

        public void ClearBasket(User currUser)
        {
            Basket currBasket = currUser.Basket;
            foreach (var b in currBasket.Books)
            {
                b.Book.Quantity++;
            }
            currBasket.Books = null;
            currBasket.TotalPrice = 0;
            currBasket.Discount = this.CheckDiscount(currUser.Basket.TotalPrice, currUser.MoneySpentBalance);
            this.Context.SaveChanges();
        }

        public void BuyBooks(User currUser)
        {
            Basket currBasket = currUser.Basket;
            currUser.MoneySpentBalance = currBasket.TotalPrice;
            Purchase currentPurchase = new Purchase()
            {
                TotalPrice = currBasket.TotalPrice,
                CompletedOndate = DateTime.Now,
                DeliveryAddress = currUser.Address,
                Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance),
                DeliveryDate = DateTime.Now.AddDays(2),
                DeliveryPrice = this.CheckDeliveryPrice(currBasket.TotalPrice),
                IsCompleted = true
            };

            var currBasketBooks = currBasket.Books.ToList();
            this.AddBooksFromCurrentBasket(currBasketBooks, currentPurchase.Books);
            //currentPurchase.Books.AddRange(currBasketBooks);

            currUser.Purchases.Add(currentPurchase);
            currBasket.Books = null;
            currBasket.TotalPrice = 0;
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            this.Context.SaveChanges();
        }

        private void AddBooksFromCurrentBasket(List<BasketBook> currBasketBooks, ICollection<BasketBook> books)
        {
            foreach (var basketBook in currBasketBooks)
            {
                books.Add(basketBook);
            }
        }

        private decimal CheckDeliveryPrice(decimal totalPrice)
        {
            decimal deliveryPrice = 5.0m;

            if (totalPrice >= 100m && totalPrice < 150m)
            {
                deliveryPrice = 3.5m;
            }

            if (totalPrice >= 150)
            {
                deliveryPrice = 2.5m;
            }

            return deliveryPrice;
        }
    }
}

using BookStore.Data;
using BookStore.Models.ViewModels;
using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;

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

            var count = basket.Books
                    .GroupBy(b => b.Book.Id)
                    .Select(b => new CountBookInBasketViewModel()
                    {
                        Count = b.Count(),
                        Book = b.First().Book,
                        BookId = b.First().Book.Id,
                        NewCount = 0
                    }).ToList();

            BasketViewModel viewModel = Mapper.Map<Basket, BasketViewModel>(basket);
            viewModel.Count = count;

            return viewModel;
        }

        private decimal CheckShippingPrice(decimal totalPrice)
        {
            decimal shippingPrice = 0;
            if (totalPrice < 50.0m)
            {
                shippingPrice = 3.5m;
            }

            if (totalPrice >= 50 && totalPrice < 150)
            {
                shippingPrice = 2.5m;
            }

            return shippingPrice;
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
                    Discount = this.CheckDiscount(currBook.Price, currUser.MoneySpentBalance)
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
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
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

        public void RemoveOneOfThisFromBasket(Book currentBook, User currUser)
        {
            Basket currBasket = currUser.Basket;
            currBasket.TotalPrice -= currentBook.Price;
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            var currBasketBooks = context.BasketsBooks
                .FirstOrDefault(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
            currBasket.Books.Remove(currBasketBooks);

            currentBook.Quantity++;
            context.SaveChanges();
        }

        public void RemoveAllOfThisFromBasket(Book currentBook, User currUser, int count)
        {
            Basket currBasket = currUser.Basket;
            currBasket.TotalPrice -= currentBook.Price * count;
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            var currBasketBooks = context.BasketsBooks
                .Where(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
            context.BasketsBooks.RemoveRange(currBasketBooks);

            currentBook.Quantity += count;
            context.SaveChanges();
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
                    this.context.BasketsBooks.Add(bookInBasket);
                    this.context.SaveChanges();
                }

                currentBook.Quantity -= difference;
                currBasket.TotalPrice += currentBook.Price * difference;
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            }

            if (newCount < currQty)
            {
                difference = currQty - newCount;
                //var editedBooks = Enumerable.Repeat(bookInBasket, newCount).ToList();
                var currBasketBooks = context.BasketsBooks
                    .Where(b => b.Basket.Id == currBasket.Id && b.Book.Id == currentBook.Id);
                this.context.BasketsBooks.RemoveRange(currBasketBooks);
                for (int i = 0; i < newCount; i++)
                {
                    this.context.BasketsBooks.Add(bookInBasket);
                    this.context.SaveChanges();
                }
              
                currBasket.TotalPrice -= currentBook.Price * difference;
                currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
                currentBook.Quantity += difference;           
            }

            this.context.SaveChanges();
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
            this.context.SaveChanges();
        }

        public void BuyBooks(User currUser)
        {
            Basket currBasket = currUser.Basket;
            currUser.MoneySpentBalance = currBasket.TotalPrice;
            currBasket.Books = null;
            currBasket.TotalPrice = 0;
            currBasket.Discount = this.CheckDiscount(currBasket.TotalPrice, currUser.MoneySpentBalance);
            this.context.SaveChanges();
        }
    }
}

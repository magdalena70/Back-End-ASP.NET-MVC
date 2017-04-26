using BookStore.Models.ViewModels.Purchase;
using System.Collections.Generic;
using AutoMapper;
using BookStore.Models.EntityModels;
using System.Linq;
using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.Book;
using System;
using BookStore.Models.BindingModels.Purchase;
using System.Data.Entity;

namespace BookStore.Services
{
    public class PurchaseService : Service
    {
        public IEnumerable<AllPurchasesViewModel> GetAll()
        {
            var purchases = this.Context.Purchases
                .OrderByDescending(p => p.CompletedOndate)
                .ThenByDescending(p => p.DeliveryDate)
                .ToList();
            IEnumerable<AllPurchasesViewModel> viewModel =
                Mapper.Map<IEnumerable<Purchase>, IEnumerable<AllPurchasesViewModel>>(purchases);

            return viewModel;
        }

        public PurchaseDetailsViewModel GetDetails(int? id)
        {
            Purchase purchase = this.Context.Purchases.Find(id);
            if (purchase == null)
            {
                return null;
            }

            var countBooks = purchase.Books
                   .GroupBy(b => b.Book.Id)
                   .Select(b => new CountBookInBasketViewModel()
                   {
                       Count = b.Count(),
                       Book = Mapper.Map<Book, BookDetailsViewModel>(b.First().Book),
                       BookId = b.First().Book.Id,
                       NewCount = 0
                   }).ToList();

            PurchaseDetailsViewModel viewModel = Mapper.Map<Purchase, PurchaseDetailsViewModel>(purchase);
            foreach (var countBook in countBooks)
            {
                this.CheckForCurrentPromotion(countBook, purchase.CompletedOndate);
                countBook.NewPrice = this.CheckNewPrice(countBook.Book.Price, countBook.PromotionDiscount);
            }

            viewModel.Books = countBooks;
            viewModel.BooksCount = this.CheckAllBooksCount(countBooks);
            return viewModel;
        }

        public EditPurchaseViewModel GetEditPurchaseViewModel(int? id)
        {
            Purchase purchase = this.Context.Purchases.Find(id);
            if (purchase == null)
            {
                return null;
            }

            EditPurchaseViewModel viewModel = Mapper.Map<Purchase, EditPurchaseViewModel>(purchase);
            return viewModel;
        }

        public void EditPurchase(EditPurchaseBindingModel bindingModel)
        {
            Purchase editedPurchase = Mapper.Map<EditPurchaseBindingModel, Purchase>(bindingModel);
            this.Context.Entry(editedPurchase).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        private int CheckAllBooksCount(List<CountBookInBasketViewModel> countBooks)
        {
            int count = 0;
            foreach (var book in countBooks)
            {
                count += book.Count;
            }

            return count;
        }

        public DeletePurchaseViewModel GetDeletePurchaseViewModel(int? id)
        {
            Purchase purchase = this.Context.Purchases.Find(id);
            if (purchase == null)
            {
                return null;
            }

            DeletePurchaseViewModel viewModel = Mapper.Map<Purchase, DeletePurchaseViewModel>(purchase);
            return viewModel;
        }

        private void CheckForCurrentPromotion(CountBookInBasketViewModel countBook, DateTime completedOndate)
        {
            foreach (var category in countBook.Book.Categories)
            {
                foreach (var promotion in category.Promotions)
                {
                    if (promotion.StartDate <= completedOndate && promotion.EndDate > completedOndate)
                    {
                        countBook.PromotionDiscount = promotion.Discount;
                    }
                }
            }
        }

        public void DeletePurchase(int id)
        {
            Purchase purchase = this.Context.Purchases.Find(id);
            User currPurchaseUser = purchase.User;
            currPurchaseUser.MoneySpentBalance -= purchase.TotalPrice;
       
            foreach (var book in purchase.Books)
            {
                book.Book.Quantity++; // book quantity in stock
            }
            this.Context.SaveChanges();

            this.Context.BasketsBooks.RemoveRange(purchase.Books);
            this.Context.SaveChanges();

            this.Context.Purchases.Remove(purchase);
            this.Context.SaveChanges();
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
    }
}

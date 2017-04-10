using System;
using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.ViewModels;
using System.Linq;
using BookStore.Models;
using BookStore.Models.EntityModels;
using AutoMapper;

namespace BookStore.Services
{
    public class BookService : Service
    {
        public BookService(BookStoreContext context) : base(context)
        {
        }

        public IEnumerable<BooksViewModel> GetNewBooks()
        {
            var newBooks = context.Books
                .Where(b => 
                    b.IssueDate.Year == DateTime.Now.Year && b.IssueDate.Month > DateTime.Now.Month - 3)
                .OrderByDescending(b => b.IssueDate)
                .ToList();

            IEnumerable<BooksViewModel> viewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<BooksViewModel>>(newBooks);
            return viewModel;
        }

        //to do
        public PromotionsViewModel GetPromotions(string categoryName, decimal discount, DateTime startDate)
        {
            var category = context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                return null;
            }

            var promotionBooks = category.Books
                .Where(b => b.IssueDate.Year == DateTime.Now.Year - 1)
                .OrderByDescending(b => b.IssueDate)
                .Select(b => new PromotionBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Authors = b.Authors.ToList(),
                    Language = b.Language,
                    CurrentPrice = b.Price,
                    Discount = discount,
                    NewPrice = b.Price - ((b.Price * 10) / 100),
                    Quantity = b.Quantity,
                    IssueDate = b.IssueDate
                })
                .ToList();
           
            PromotionsViewModel viewModel = new PromotionsViewModel()
            {
                Category = categoryName,
                Discount = discount,
                StartDate = startDate,
                EndDate = startDate.AddDays(30),
                Books = promotionBooks
            };

            return viewModel;
        }

        public BookDetailsViewModel GetDetails(int? id)
        {
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return null;
            }

            BookDetailsViewModel viewModel = Mapper.Map<Book, BookDetailsViewModel>(book);
            return viewModel;
        }

        public IEnumerable<BooksViewModel> GetBooksByTitle(string bookTitle)
        {
            var books = context.Books
                .Include("Authors")
                .Where(b => b.Title.Contains(bookTitle))
                .ToList();
            if (books.Count() == 0)
            {
                return null;
            }

            IEnumerable<BooksViewModel> viewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<BooksViewModel>>(books);
            return viewModel;
        }
    }
}

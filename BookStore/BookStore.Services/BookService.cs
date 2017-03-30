using System;
using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.ViewModels;
using System.Linq;
using BookStore.Models;

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
                .Where(b => (b.IssueDate.Year == DateTime.Now.Year && b.IssueDate.Month == DateTime.Now.Month) ||
                            (b.IssueDate.Year == DateTime.Now.Year && (b.IssueDate.Month)-1 == (DateTime.Now.Month)-1) ||
                            (b.IssueDate.Year == DateTime.Now.Year && (b.IssueDate.Month) - 2 == (DateTime.Now.Month) - 2))
                .OrderByDescending(b => b.IssueDate)
                .Select(b => new BooksViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Authors = b.Authors.ToList(),
                    Language = b.Language,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    IssueDate = b.IssueDate
                })
                .ToList();

            return newBooks;
        }

        public BookDetailsViewModel GetDetails(int? id)
        {
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return null;
            }

            BookDetailsViewModel viewModel = new BookDetailsViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                IssueDate = book.IssueDate,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                ISBN = book.ISBN,
                NumberOfPages = book.NumberOfPages,
                Price = book.Price,
                Language = book.Language,
                Quantity = book.Quantity,
                Categories = book.Categories.ToList(),
                Authors = book.Authors.ToList(),
                Reviews = book.Reviews.ToList(),
                UpRating = book.UpRating,
                DownRating = book.DownRating
            };

            return viewModel;
        }

        public IEnumerable<BooksViewModel> GetBooksByTitle(string bookTitle)
        {
            var books = context.Books
                .Where(b => b.Title.Contains(bookTitle))
                .Select(b => new BooksViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    IssueDate = b.IssueDate,
                    Authors = b.Authors.ToList(),
                    Language = b.Language,
                    Price = b.Price,
                    Quantity = b.Quantity
                })
                .ToList();
            if (books.Count() == 0)
            {
                return null;
            }

            return books;
        }
    }
}

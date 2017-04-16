using System;
using System.Collections.Generic;
using BookStore.Models.ViewModels;
using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;

namespace BookStore.Services
{
    public class BookService : Service
    {

        public IEnumerable<BooksViewModel> GetNewBooks()
        {
            var newBooks = this.Context.Books
                .Where(b => 
                    b.IssueDate.Year == DateTime.Now.Year && b.IssueDate.Month > DateTime.Now.Month - 3)
                .OrderByDescending(b => b.IssueDate)
                .ToList();

            IEnumerable<BooksViewModel> viewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<BooksViewModel>>(newBooks);
            return viewModel;
        }

        public BookDetailsViewModel GetDetails(int? id)
        {
            Book book = this.Context.Books.Find(id);
            if (book == null)
            {
                return null;
            }

            BookDetailsViewModel viewModel = Mapper.Map<Book, BookDetailsViewModel>(book);
            return viewModel;
        }

        public IEnumerable<BooksViewModel> GetBooksByTitle(string bookTitle)
        {
            var books = this.Context.Books
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

using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.BindingModels.Book;
using System.Data.Entity;

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

        public IEnumerable<AllBooksViewModel> GetAll(int page, int count)
        {
            var books = this.Context.Books
                .Include("Authors")
                .OrderBy(b => b.Title)
                .ThenByDescending(b => b.IssueDate)
                .Skip(page - 1)
                .Take(count);

            IEnumerable<AllBooksViewModel> viewModel = Mapper.Map<IEnumerable<Book>, IEnumerable<AllBooksViewModel>>(books);
            return viewModel;
        }

        public int GetAllBooksCount()
        {
            int count = this.Context.Books.Count();
            return count;
        }

        public BookDetailsViewModel GetDetails(int? id)
        {
            Book book = this.Context.Books.Find(id);
            if (book == null)
            {
                return null;
            }

            BookDetailsViewModel viewModel = Mapper.Map<Book, BookDetailsViewModel>(book);
            int bookPurchasesCount = this.Context.BasketsBooks
                .Where(b => b.Book.Id == id).Count();
            viewModel.PurchasesCount = bookPurchasesCount;

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

        public void AddBook(AddBookBindingModel bindingModel)
        {
            Book newBook = Mapper.Map<AddBookBindingModel, Book>(bindingModel);
            this.Context.Books.Add(newBook);
            this.Context.SaveChanges();
        }

        public Book GetNewBooks(string title, string iSBN)
        {
            Book newBook = this.Context.Books
                .FirstOrDefault(b => b.Title == title && b.ISBN == iSBN);

            return newBook;
        }

        public EditBookViewModel GetEditBookViewModel(int? id)
        {
            Book currentBook = this.Context.Books.Find(id);
            if (currentBook == null)
            {
                return null;
            }

            EditBookViewModel viewModel = Mapper.Map<Book, EditBookViewModel>(currentBook);
            return viewModel;
        }

        public void GetEditBook(EditBookBindingModel bindingModel)
        {
            Book editedBook = Mapper.Map<EditBookBindingModel, Book>(bindingModel);
            this.Context.Entry(editedBook).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public DeleteBookViewModel GetDeliteBookViewModel(int? id)
        {
            Book book = this.Context.Books.Find(id);
            DeleteBookViewModel viewModel = Mapper.Map<Book, DeleteBookViewModel>(book);
            if (viewModel == null)
            {
                return null;
            }

            return viewModel;
        }

        public Book GetCurrentBook(int id)
        {
            Book book = this.Context.Books.Find(id);
            return book;
        }

        public void DeleteBook(Book book)
        {
            Book currentBook = this.GetCurrentBook(book.Id);
            this.Context.Books.Remove(currentBook);
            this.Context.SaveChanges();
        }
    }
}

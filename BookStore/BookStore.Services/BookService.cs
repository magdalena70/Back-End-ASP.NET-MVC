using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.BindingModels.Book;
using System.Data.Entity;
using System.Web.Mvc;
using BookStore.Models.ViewModels.Rating;

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

        public BookDetailsViewModel GetDetails(int? id, string currUserId)
        {
            Book book = this.Context.Books.Find(id);
            if (book == null)
            {
                return null;
            }

            BookDetailsViewModel viewModel = Mapper.Map<Book, BookDetailsViewModel>(book);
            var authors = this.Context.Authors
               .Select(a => new SelectListItem()
               {
                   Value = a.Id.ToString(),
                   Text = a.FullName
               })
               .ToList();

            var categories = this.Context.Categories
               .Select(c => new SelectListItem()
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();

            int bookPurchasesCount = this.Context.BasketsBooks
                .Where(b => b.Book.Id == id).Count();

            viewModel.SelectAuthors = authors;
            viewModel.SelectCategories = categories;
            viewModel.PurchasesCount = bookPurchasesCount;
            viewModel.Reviews = viewModel.Reviews.OrderByDescending(r => r.DateCreate).ToList();
            viewModel.IsCurrentUserRated = this.CheckIfCurrentUserRated(viewModel.Ratings, currUserId);
            viewModel.CurrentUserRatingValue = this.GetCurrentUserRatingValue(viewModel.Ratings, currUserId);
            viewModel.AvgRating = this.CalculateAvgRating(viewModel.Ratings);

            return viewModel;
        }

        private double CalculateAvgRating(List<RatingViewModel> ratings)
        {
            double avgRating = 0;
            if (ratings.Sum(r => r.Value) > 0)
            {
                avgRating = ratings.Average(r => r.Value);
            }

            return avgRating;
        }

        private int GetCurrentUserRatingValue(List<RatingViewModel> ratings, string currUserId)
        {
            int ratingValue = 0;
            foreach (var rating in ratings)
            {
                if (rating.UserId == currUserId)
                {
                    ratingValue = rating.Value;
                }
            }

            return ratingValue;
        }

        private bool CheckIfCurrentUserRated(List<RatingViewModel> ratings, string currUserId)
        {
            bool isRated = false;
            foreach (var rating in ratings)
            {
                if (rating.UserId == currUserId)
                {
                    isRated = true;
                }
            }

            return isRated;
        }

        public void AddCategoryToBook(AddCategoryToBookBindingModel bindingModel)
        {
            Book book = this.Context.Books.Find(bindingModel.Id);
            Category category = this.Context.Categories
                .First(c => c.Id.ToString() == bindingModel.SelectCategories);
            book.Categories.Add(category);
            this.Context.SaveChanges();
        }

        public void AddAuthorToBook(AddAuthorToBookBindingModel bindingModel)
        {
            Book book = this.Context.Books.Find(bindingModel.Id);
            Author author = this.Context.Authors
                .First(a => a.Id.ToString() == bindingModel.SelectAuthors);
            book.Authors.Add(author);
            this.Context.SaveChanges();
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

        public AddBookViewModel GetAddBookViewModel()
        {
            var authors = this.Context.Authors
               .Select(a => new SelectListItem()
               {
                   Value = a.Id.ToString(),
                   Text = a.FullName
               })
               .ToList();

            var categories = this.Context.Categories
               .Select(c => new SelectListItem()
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();

            AddBookViewModel viewModel = new AddBookViewModel()
            {
                Categories = categories,
                Authors = authors
            };

            return viewModel;
        }

        public void AddBook(AddBookBindingModel bindingModel)
        {
            Author author = this.Context.Authors
                .First(a => a.Id.ToString() == bindingModel.Authors);
            bindingModel.Authors = null;

            Category category = this.Context.Categories
               .First(c => c.Id.ToString() == bindingModel.Categories);
            bindingModel.Categories = null;

            Book newBook = Mapper.Map<AddBookBindingModel, Book>(bindingModel);
            newBook.Categories.Add(category);
            newBook.Authors.Add(author);
            

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

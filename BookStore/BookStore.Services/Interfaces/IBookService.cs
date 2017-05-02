using System.Collections.Generic;
using BookStore.Models.BindingModels.Book;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Book;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        void AddAuthorToBook(AddAuthorToBookBindingModel bindingModel);
        void AddBook(AddBookBindingModel bindingModel);
        void AddCategoryToBook(AddCategoryToBookBindingModel bindingModel);
        void DeleteBook(Book book);
        AddBookViewModel GetAddBookViewModel();
        IEnumerable<AllBooksViewModel> GetAll(int page, int count);
        int GetAllBooksCount();
        IEnumerable<BooksViewModel> GetBooksByTitle(string bookTitle);
        Book GetCurrentBook(int id);
        DeleteBookViewModel GetDeliteBookViewModel(int? id);
        BookDetailsViewModel GetDetails(int? id, string currUserId);
        void GetEditBook(EditBookBindingModel bindingModel);
        EditBookViewModel GetEditBookViewModel(int? id);
        IEnumerable<BooksViewModel> GetNewBooks();
        Book GetNewBooks(string title, string iSBN);
    }
}
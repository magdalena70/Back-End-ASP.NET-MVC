using System.Collections.Generic;
using BookStore.Models.BindingModels.Author;
using BookStore.Models.ViewModels.Author;

namespace BookStore.Services.Interfaces
{
    public interface IAuthorService
    {
        void AddAuthor(AddAuthorBindingModel bindingModel);
        void DeleteAuthor(int id);
        void EditAuthor(EditAuthorBindingModel bindingModel);
        IEnumerable<AuthorViewModel> GetAll();
        IEnumerable<AuthorViewModel> GetAllByName(string authorName);
        AuthorViewModel GetAuthor(int? id);
        AuthorWithBooksViewModel GetAuthorDetails(int? id);
        AuthorWithBooksViewModel GetAuthorWithBooks(string authorName);
        AuthorViewModel GetCurrentAuthor(string fullName);
        bool IsAuthorExists(string fullName);
    }
}
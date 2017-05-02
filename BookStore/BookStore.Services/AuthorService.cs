using System.Linq;
using System.Collections.Generic;
using BookStore.Models.EntityModels;
using AutoMapper;
using BookStore.Models.ViewModels.Author;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.BindingModels.Author;
using System.Web;
using System.Data.Entity;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class AuthorService : Service, IAuthorService
    {

        public IEnumerable<AuthorViewModel> GetAll()
        {
            var allAuthors = this.Context.Authors.ToList();

            IEnumerable<AuthorViewModel> viewModel = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(allAuthors);
            return viewModel;
        }

        public AuthorViewModel GetAuthor(int? id)
        {
            Author author = this.Context.Authors.Find(id);
            if (author == null)
            {               
                return null;
            }

            AuthorViewModel viewModel = Mapper.Map<Author, AuthorViewModel>(author);
            return viewModel;
        }

        public AuthorWithBooksViewModel GetAuthorWithBooks(string authorName)
        {
            authorName = HttpUtility.HtmlDecode(authorName.Trim());

            Author author = this.Context.Authors
                .Include("Books")
                .FirstOrDefault(a => a.FullName.Contains(authorName));
            if (author == null)
            {
                return null;
            }

            AuthorWithBooksViewModel viewModel = Mapper.Map<Author, AuthorWithBooksViewModel>(author);
            var authorBooks = author.Books.OrderByDescending(b => b.IssueDate).ToList();
            viewModel.Books = Mapper.Map<ICollection<Book>, ICollection<BooksViewModel>>(authorBooks);
            return viewModel;
        }

        public AuthorWithBooksViewModel GetAuthorDetails(int? id)
        {
            Author author = this.Context.Authors.Find(id);
            if (author == null)
            {
                return null;
            }

            AuthorWithBooksViewModel viewModel = Mapper.Map<Author, AuthorWithBooksViewModel>(author);
            return viewModel;
        }

        public IEnumerable<AuthorViewModel> GetAllByName(string authorName)
        {
            authorName = HttpUtility.HtmlDecode(authorName.Trim());
            var allAuthors = this.Context.Authors
                .Where(a => a.FullName.Contains(authorName))
                .ToList();

            IEnumerable<AuthorViewModel> viewModel = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(allAuthors);
            return viewModel;
        }

        public AuthorViewModel GetCurrentAuthor(string fullName)
        {
            fullName = HttpUtility.HtmlDecode(fullName);
            Author author = this.Context.Authors
                .FirstOrDefault(a => a.FullName == fullName);
            if (author == null)
            {
                return null;
            }

            AuthorViewModel viewModel = Mapper.Map<Author, AuthorViewModel>(author);
            return viewModel;
        }

        public bool IsAuthorExists(string fullName)
        { 
            fullName = HttpUtility.HtmlDecode(fullName.Trim());

            var author = this.Context.Authors
                .FirstOrDefault(a => a.FullName == fullName);
            if (author != null)
            {
                return true;
            }

            return false;
        }

        public void AddAuthor(AddAuthorBindingModel bindingModel)
        {
            var newAuthor = Mapper.Map<AddAuthorBindingModel, Author>(bindingModel);
            this.Context.Authors.Add(newAuthor);
            this.Context.SaveChanges();
        }

        public void EditAuthor(EditAuthorBindingModel bindingModel)
        {
            var author = Mapper.Map<EditAuthorBindingModel, Author>(bindingModel);
            this.Context.Entry(author).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            Author author = this.Context.Authors.Find(id);
            this.Context.Authors.Remove(author);
            this.Context.SaveChanges();
        }
    }
}

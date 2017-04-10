using BookStore.Data;
using BookStore.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using BookStore.Models.EntityModels;
using AutoMapper;

namespace BookStore.Services
{
    public class AuthorService : Service
    {
        public AuthorService(BookStoreContext context) : base(context)
        {
        }

        public IEnumerable<AuthorViewModel> GetAll()
        {
            var allAuthors = context.Authors.ToList();

            IEnumerable<AuthorViewModel> viewModel = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(allAuthors);
            return viewModel;
        }

        public AuthorViewModel GetAuthor(int? id)
        {
            Author author = context.Authors.Find(id);
            if (author == null)
            {
                
                return null;
            }

            AuthorViewModel viewModel = Mapper.Map<Author, AuthorViewModel>(author);
            return viewModel;
        }

        public AuthorWithBooksViewModel GetAuthorWithBooks(string authorName)
        {
            Author author = context.Authors
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
    }
}

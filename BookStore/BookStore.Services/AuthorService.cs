using BookStore.Data;
using BookStore.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using BookStore.Models;

namespace BookStore.Services
{
    public class AuthorService : Service
    {
        public AuthorService(BookStoreContext context) : base(context)
        {
        }

        public IEnumerable<AllAuthorsViewModel> GetAll()
        {
            var allAuthors = context.Authors
                .Select(a => new AllAuthorsViewModel()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Bio = a.Bio
                })
                .ToList();

            return allAuthors;
        }

        public AuthorViewModel GetAuthor(int? id)
        {
            Author author = context.Authors.Find(id);
            if (author == null)
            {
                
                return null;
            }

            AuthorViewModel viewModel = new AuthorViewModel()
            {
                FullName = author.FullName,
                Bio = author.Bio
            };

            return viewModel;
        }

        public AuthorWithBooksViewModel GetAuthorWithBooks(string authorName)
        {
            Author author = context.Authors
                .FirstOrDefault(a => a.FullName.Contains(authorName));
            if (author == null)
            {
                return null;
            }

            AuthorWithBooksViewModel viewModel = new AuthorWithBooksViewModel()
            {
                FullName = author.FullName,
                Books = author.Books.ToList(),
                BooksCount = author.Books.Count
            };

            return viewModel;
        }
    }
}

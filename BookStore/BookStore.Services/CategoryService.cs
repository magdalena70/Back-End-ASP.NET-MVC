using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;

namespace BookStore.Models
{
    public class CategoryService : Service
    {
      
        public IEnumerable<AllCategoriesViewModel> GetAll()
        {
            var categories = this.Context.Categories
                .ToList();

            IEnumerable<AllCategoriesViewModel> viewModel = Mapper.Map<IEnumerable<Category>, IEnumerable<AllCategoriesViewModel>>(categories);
            return viewModel;
        }

        public CategoryViewModel GetBestSellers()
        {
            var category = this.Context.Categories
                .Include("Books")
                .FirstOrDefault(c => c.Name == "Best Sellers");
            if (category == null)
            {
                return null;
            }

            var categoryBooks = category.Books
                    .OrderByDescending(b => b.IssueDate)
                    .ToList();

            CategoryViewModel viewModel = Mapper.Map<Category, CategoryViewModel>(category);
            ICollection<BooksViewModel> booksViewModel = Mapper.Map<ICollection<Book>, ICollection<BooksViewModel>>(categoryBooks);
            viewModel.Books = booksViewModel;

            return viewModel;
        }

        public CategoryViewModel GetCategoryDetails(int? id)
        {
            var category = this.Context.Categories.Find(id);
            if (category == null)
            {
                return null;
            }

            var categoryBooks = category.Books.ToList();

            CategoryViewModel viewModel = Mapper.Map<Category, CategoryViewModel>(category);
            ICollection<BooksViewModel> booksViewModel = Mapper.Map<ICollection<Book>, ICollection<BooksViewModel>>(categoryBooks);
            viewModel.Books = booksViewModel;

            return viewModel;
        }

        public CategoryViewModel GetCategoryByName(string categoryName)
        {
            var category = this.Context.Categories
                .Include("Books")
               .FirstOrDefault(c => c.Name.Contains(categoryName));
            if (category == null)
            {
                return null;
            }

            var categoryBooks = category.Books.ToList();

            CategoryViewModel viewModel = Mapper.Map<Category, CategoryViewModel>(category);
            ICollection<BooksViewModel> booksViewModel = Mapper.Map<ICollection<Book>, ICollection<BooksViewModel>>(categoryBooks);
            viewModel.Books = booksViewModel;

            return viewModel;
        }
    }
}

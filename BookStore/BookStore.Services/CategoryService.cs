using System;
using BookStore.Data;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models
{
    public class CategoryService : Service
    {
        public CategoryService(BookStoreContext context) : base(context)
        {
        }

        public List<AllCategoriesViewModel> GetAll()
        {
            var viewModel = this.context.Categories
                .Select(c => new AllCategoriesViewModel()
                {
                    Name = c.Name,
                    Id = c.Id
                })
                .ToList();

            return viewModel;
        }

        public CategoryViewModel GetBestSellers()
        {
            var category = this.context.Categories
                .FirstOrDefault(c => c.Name == "Best Sellers");
            CategoryViewModel viewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Books = category.Books.ToList()
            };
                
            return viewModel;
        }

        public CategoryViewModel GetCategoryDetails(int? id)
        {
            var category = this.context.Categories.Find(id);
            CategoryViewModel viewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Books = category.Books.ToList()
            };

            return viewModel;
        }

        public CategoryViewModel GetCategoryByName(string categoryName)
        {
            var category = context.Categories
               .FirstOrDefault(c => c.Name.Contains(categoryName));
            CategoryViewModel viewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Books = category.Books.ToList()
            };

            return viewModel;
        }
    }
}

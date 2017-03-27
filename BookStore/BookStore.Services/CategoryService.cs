﻿using BookStore.Data;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models.BindingModels;

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
                .Include("Books")
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
            var category = this.context.Categories
                .Find(id);
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
                .Include("Books")
               .FirstOrDefault(c => c.Name.Contains(categoryName));
            CategoryViewModel viewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Books = category.Books.ToList()
            };

            return viewModel;
        }

        public Category CreateCategory(CategoryBindingModel bindingModel)
        {
            Category category = new Category()
            {
                Name = bindingModel.Name
            };

            return category;
        }

        public Category EditCategory(CategoryBindingModel bindingModel)
        {
            Category category = new Category()
            {
                Id = bindingModel.Id,
                Name = bindingModel.Name
            };

            return category;
        }

        public void DeleteCategory(int id)
        {
            Category category = this.context.Categories.Find(id);
            this.context.Categories.Remove(category);
            this.context.SaveChanges();
        }
    }
}

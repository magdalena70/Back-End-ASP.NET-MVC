using BookStore.Services;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models.EntityModels;
using AutoMapper;
using System.Data.Entity;
using BookStore.Models.ViewModels.Category;
using BookStore.Models.BindingModels.Category;
using BookStore.Models.ViewModels.Book;
using System;
using System.Web;

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

        public IEnumerable<AllCategoriesViewModel> GetAllByName(string categoryName)
        {
            categoryName = HttpUtility.HtmlDecode(categoryName.Trim());
            var categories = this.Context.Categories
                .Where(c => c.Name.Contains(categoryName))
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

        public bool IsCategoryExists(string name)
        {
            var category = this.GetCategoryByName(name);
            if (category != null)
            {
                return true;
            }

            return false;
        }

        public CategoryViewModel GetCurrentCategory(string categoryName)
        {
            categoryName = HttpUtility.HtmlDecode(categoryName.Trim());
            var category = this.Context.Categories
                .Include("Books")
                .Include("Promotions")
               .FirstOrDefault(c => c.Name == categoryName);
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
            categoryName = HttpUtility.HtmlDecode(categoryName.Trim());
            var category = this.Context.Categories
                .Include("Books")
                .Include("Promotions")
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

        public void AddNewCategory(AddCategoryBindingModel bindingModel)
        {
            Category category = Mapper.Map<AddCategoryBindingModel, Category>(bindingModel);
            this.Context.Categories.Add(category);
            this.Context.SaveChanges();
        }

        public EditCategoryViewModel GetCategoryViewModel(int? id)
        {
            Category category = this.Context.Categories.Find(id);
            if (category == null)
            {
                return null;
            }

            EditCategoryViewModel viewModel = Mapper.Map<Category, EditCategoryViewModel>(category);
            return viewModel;
        }

        public void EditCategory(EditCategoryBindingModel bindingModel)
        {
            Category editedCategory = Mapper.Map<EditCategoryBindingModel, Category>(bindingModel);
            this.Context.Entry(editedCategory).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public Category GetCurrentCategory(int? id)
        {
            Category currentCategory = this.Context.Categories.Find(id);
            if (currentCategory == null)
            {
                return null;
            }

            return currentCategory;
        }

        public void DeleteCategory(int id)
        {
            Category currentCategory = this.GetCurrentCategory(id);
            this.Context.Categories.Remove(currentCategory);
            this.Context.SaveChanges();
        }

        public string GetCategoryName(int id)
        {
            string categoryName = this.Context.Categories.Find(id).Name;
            return categoryName;
        }
    }
}

using System.Collections.Generic;
using BookStore.Models.BindingModels.Category;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Category;

namespace BookStore.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddNewCategory(AddCategoryBindingModel bindingModel);
        void DeleteCategory(int id);
        void EditCategory(EditCategoryBindingModel bindingModel);
        IEnumerable<AllCategoriesViewModel> GetAll();
        IEnumerable<AllCategoriesViewModel> GetAllByName(string categoryName);
        CategoryViewModel GetBestSellers();
        CategoryViewModel GetCategoryByName(string categoryName);
        CategoryViewModel GetCategoryDetails(int? id);
        string GetCategoryName(int id);
        EditCategoryViewModel GetCategoryViewModel(int? id);
        CategoryViewModel GetCurrentCategory(string categoryName);
        Category GetCurrentCategory(int? id);
        DeleteCategoryViewModel GetDeleteCategoryViewModel(int? id);
        bool IsCategoryExists(string name);
    }
}
using System.Linq;
using System.Collections.Generic;
using BookStore.Models.EntityModels;
using AutoMapper;
using System.Data.Entity;
using BookStore.Models.ViewModels.User;
using BookStore.Models.BindingModels.Book;
using BookStore.Models.ViewModels.Category;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Basket;

namespace BookStore.Services
{
    public class UserService : Service
    {
        
        public UserProfileViewModel GetUserProfileViewModel(User currentUser)
        {
            UserProfileViewModel viewModel = Mapper.Map<User, UserProfileViewModel>(currentUser);

            if (currentUser.Basket != null)
            {

                viewModel.Basket = Mapper.Map<Basket, BasketViewModel>(currentUser.Basket);
                viewModel.BasketTotalPrice = currentUser.Basket.TotalPrice;
                viewModel.CountBooksInBasket = currentUser.Basket.Books.Count;
                viewModel.Books = currentUser.Basket.Books
                    .GroupBy(b => b.Book.Id)
                    .Select(b => new CountBookInBasketViewModel()
                    {
                        Count = b.Count(),
                        Book = Mapper.Map<Book, BookDetailsViewModel>(b.First().Book)
                    }).ToList();
           
                viewModel.YouMayAlsoLike = GetCategories(currentUser);
            }

            return viewModel;
        }

        public User GetCurrentUser(string authenticatedUserId)
        {
            User currentUser = this.Context.Users.Find(authenticatedUserId);
            return currentUser;
        }

        private ICollection<AllCategoriesViewModel> GetCategories(User currentUser)
        {
            List<BasketBook> booksInBasket = currentUser.Basket.Books.ToList();
            List<Category> categoryInBasket = new List<Category>();
            foreach (var bookInBasket in booksInBasket)
            {
                foreach (var category in bookInBasket.Book.Categories)
                {
                    if (!categoryInBasket.Contains(category))
                    {
                        categoryInBasket.Add(category);
                    }
                }
            }

            ICollection<AllCategoriesViewModel> categoryInBasketViewModel = Mapper.Map<ICollection<Category>, ICollection<AllCategoriesViewModel>>(categoryInBasket);
            return categoryInBasketViewModel;
        }

        public UserFavoriteBooksViewModel GetFavorite(User currentUser)
        {
            var currUserFavorite = currentUser.FavoriteBooks.ToList();

            UserFavoriteBooksViewModel viewModel = Mapper.Map<User, UserFavoriteBooksViewModel>(currentUser);
            ICollection<BooksViewModel> favoriteViewModel = Mapper.Map<ICollection<Book>, ICollection<BooksViewModel>>(currUserFavorite);
            viewModel.FavoriteBooks = favoriteViewModel;

            return viewModel;
        }

        public void AddBookToFavoriteBooks(User currentUser, int bookId)
        {
            Book currentBook = this.Context.Books.Find(bookId);
            currentUser.FavoriteBooks.Add(currentBook);
            this.Context.SaveChanges();
        }

        public void RemoveBookFromFavoriteBooks(User currentUser, int bookId)
        {
            var userId = currentUser.Id;
            Book currentBook = this.Context.Books.Find(bookId);
            currentUser.FavoriteBooks.Remove(currentBook);

            this.Context.SaveChanges();
        }

        public EditUserProfileViewModel GetEditUserProfileViewModel(User currentUser)
        {
            EditUserProfileViewModel viewModel = Mapper.Map<User, EditUserProfileViewModel>(currentUser);
            return viewModel;
        }

        public void EditUserProfile(User currentUser, EditUserProfileBindingModel bindingModel)
        {
            currentUser.FirstName = bindingModel.FirstName;
            currentUser.LastName = bindingModel.LastName;
            currentUser.Address = bindingModel.Address;
            currentUser.PhoneNumber = bindingModel.PhoneNumber;
            currentUser.Email = bindingModel.Email;
            currentUser.UserName = bindingModel.UserName;

            this.Context.Entry(currentUser).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
    }
}

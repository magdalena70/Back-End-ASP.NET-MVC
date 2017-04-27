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
using System;

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

        public UserDetailsViewModel GetUserDetails(string username)
        {
            User user = this.Context.Users
                .Include("Basket")
                .Include("FavoriteBooks")
                .Include("Purchases")
                .FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return null;
            }

            UserDetailsViewModel viewModel = Mapper.Map<User, UserDetailsViewModel>(user);
            return viewModel;

        }

        public IEnumerable<AllUsersViewModel> GetAll()
        {
            var users = this.Context.Users.ToList();
            if (users.Count() == 0)
            {
                return null;
            }

            IEnumerable<AllUsersViewModel> viewModel = 
                Mapper.Map<IEnumerable<User>, IEnumerable<AllUsersViewModel>> (users);

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

        public void EditUserMoneySpentBalance(EditUserMoneySpentBalanceBindingModel bindingModel)
        {
            User editedUser = this.Context.Users.Find(bindingModel.Id);
            editedUser.MoneySpentBalance = bindingModel.MoneySpentBalance;
            this.Context.SaveChanges();
        }

        public void DeleteUser(string username)
        {
            User user = this.Context.Users.FirstOrDefault(u => u.UserName == username);
            if (user.Purchases.Count > 0)
            {
                foreach (var purchase in user.Purchases)
                {
                    this.Context.BasketsBooks.RemoveRange(purchase.Books);
                }
            }

            this.Context.Purchases.RemoveRange(user.Purchases);
            //this.Context.Books.RemoveRange(user.FavoriteBooks);
            this.Context.Baskets.Remove(user.Basket);
            this.Context.Users.Remove(user);
            this.Context.SaveChanges();
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

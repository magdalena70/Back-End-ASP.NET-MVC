﻿using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using BookStore.Models.BindingModels;

namespace BookStore.Services
{
    public class UserService : Service
    {
        public UserService(BookStoreContext context) : base(context)
        {
        }

        public UserProfileViewModel GetUserProfileViewModel(User currentUser)
        {
            UserProfileViewModel viewModel = new UserProfileViewModel()
            {
                UserName = currentUser.UserName,
                Basket = currentUser.Basket,
                BasketTotalPrice = currentUser.Basket.TotalPrice,
                CountBooksInBasket = currentUser.Basket.Books.Count,
                BooksInBasket = currentUser.Basket.Books.ToList(),
                YouMayAlsoLike = GetCategories(currentUser)
            };

            return viewModel;
        }

        private List<Category> GetCategories(User currentUser)
        {
            List<Book> booksInBasket = currentUser.Basket.Books.ToList();
            List<Category> categoryInBasket = new List<Category>();
            foreach (var bookInBasket in booksInBasket)
            {
                foreach (var category in bookInBasket.Categories)
                {
                    if (!categoryInBasket.Contains(category))
                    {
                        categoryInBasket.Add(category);
                    }                 
                }
            }

            return categoryInBasket;
        }

        public UserFavoriteBooksViewModel GetFavorite(User currentUser)
        {
            UserFavoriteBooksViewModel favorite = new UserFavoriteBooksViewModel()
            {
                UserName = currentUser.UserName,
                FavoriteBooks = currentUser.FavoriteBooks.ToList()
            };

            return favorite;
        }

        public void AddBookToFavoriteBooks(User currentUser, int bookId)
        {
            Book currentBook = this.context.Books.Find(bookId);
            currentUser.FavoriteBooks.Add(currentBook);
            this.context.SaveChanges();
        }

        public void RemoveBookFromFavoriteBooks(User currentUser, int bookId)
        {
            var userId = currentUser.Id;
            Book currentBook = this.context.Books.Find(bookId);
            currentUser.FavoriteBooks.Remove(currentBook);

            this.context.SaveChanges();
        }

        public EditUserProfileViewModel GetEditUserProfileViewModel(User currentUser)
        {
            EditUserProfileViewModel viewModel = new EditUserProfileViewModel()
            {
                Id = currentUser.Id,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Address = currentUser.Address,
                UserName = currentUser.UserName,
                PhoneNumber = currentUser.PhoneNumber,
                Email = currentUser.Email
            };

            return viewModel;
        }

        public User EditUserProfile(User currentUser, EditUserProfileBindingModel bindingModel)
        {
            currentUser.FirstName = bindingModel.FirstName;
            currentUser.LastName = bindingModel.LastName;
            currentUser.Address = bindingModel.Address;
            currentUser.PhoneNumber = bindingModel.PhoneNumber;
            currentUser.Email = bindingModel.Email;
            currentUser.UserName = bindingModel.UserName;

            return currentUser;
        }
    }
}
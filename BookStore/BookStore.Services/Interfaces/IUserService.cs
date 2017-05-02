using System.Collections.Generic;
using BookStore.Models.BindingModels.Book;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.User;

namespace BookStore.Services.Interfaces
{
    public interface IUserService
    {
        void AddBookToFavoriteBooks(User currentUser, int bookId);
        void DeleteUser(string username);
        void EditUserMoneySpentBalance(EditUserMoneySpentBalanceBindingModel bindingModel);
        void EditUserProfile(User currentUser, EditUserProfileBindingModel bindingModel);
        IEnumerable<AllUsersViewModel> GetAll();
        User GetCurrentUser(string authenticatedUserId);
        EditUserProfileViewModel GetEditUserProfileViewModel(User currentUser);
        UserFavoriteBooksViewModel GetFavorite(User currentUser);
        UserDetailsViewModel GetUserDetails(string username);
        UserProfileViewModel GetUserProfileViewModel(User currentUser);
        void RemoveBookFromFavoriteBooks(User currentUser, int bookId);
    }
}
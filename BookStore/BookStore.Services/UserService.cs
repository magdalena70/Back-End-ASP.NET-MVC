using BookStore.Data;
using BookStore.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using BookStore.Models.BindingModels;
using BookStore.Models.EntityModels;
using AutoMapper;

namespace BookStore.Services
{
    public class UserService : Service
    {
        public UserService(BookStoreContext context) : base(context)
        {
        }

        public UserProfileViewModel GetUserProfileViewModel(User currentUser)
        {
            UserProfileViewModel viewModel = Mapper.Map<User, UserProfileViewModel>(currentUser);

            if (currentUser.Basket != null)
            {
                viewModel.Basket = currentUser.Basket;
                viewModel.BasketTotalPrice = currentUser.Basket.TotalPrice;
                viewModel.CountBooksInBasket = currentUser.Basket.Books.Count;
                viewModel.Books = currentUser.Basket.Books
                    .GroupBy(b => b.Book.Id)
                    .Select(b => new CountBookInBasketViewModel()
                    {
                        Count = b.Count(),
                        Book = b.First().Book
                    }).ToList();
           
                viewModel.YouMayAlsoLike = GetCategories(currentUser);
            }

            return viewModel;
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
            EditUserProfileViewModel viewModel = Mapper.Map<User, EditUserProfileViewModel>(currentUser);
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

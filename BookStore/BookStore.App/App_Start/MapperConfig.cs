using AutoMapper;
using BookStore.Models.BindingModels.Author;
using BookStore.Models.BindingModels.Book;
using BookStore.Models.BindingModels.Category;
using BookStore.Models.BindingModels.Promotion;
using BookStore.Models.BindingModels.Purchase;
using BookStore.Models.BindingModels.Review;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Author;
using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Category;
using BookStore.Models.ViewModels.Home;
using BookStore.Models.ViewModels.Promotion;
using BookStore.Models.ViewModels.Purchase;
using BookStore.Models.ViewModels.Review;
using BookStore.Models.ViewModels.User;
using System;
using System.Linq;

namespace BookStore.App.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(ex =>
            {
                ex.CreateMap<Author, AuthorViewModel>();
                ex.CreateMap<Author, AuthorWithBooksViewModel>()
                    .ForMember(vm => vm.BooksCount, options => options.MapFrom(a => a.Books.Count));
                ex.CreateMap<AddAuthorBindingModel, Author>();
                ex.CreateMap<EditAuthorBindingModel, Author>();

                ex.CreateMap<Book, BookDetailsViewModel>();
                ex.CreateMap<Book, BooksViewModel>();
                ex.CreateMap<Book, AllBooksViewModel>();
                ex.CreateMap<Book, AddBookViewModel>();
                ex.CreateMap<AddBookBindingModel, Book>();
                ex.CreateMap<Book, EditBookViewModel>();
                ex.CreateMap<EditBookBindingModel, Book>();
                ex.CreateMap<Book, DeleteBookViewModel>();

                ex.CreateMap<Category, AllCategoriesViewModel>();
                ex.CreateMap<Category, CategoryViewModel>();
                ex.CreateMap<AddCategoryBindingModel, Category>();
                ex.CreateMap<Category, EditCategoryViewModel>();
                ex.CreateMap<EditCategoryBindingModel, Category>();
                ex.CreateMap<Category, DeleteCategoryViewModel>();

                ex.CreateMap<User, AllUsersViewModel>();
                ex.CreateMap<User, UserDetailsViewModel>();
                ex.CreateMap<User, UserFavoriteBooksViewModel>();
                ex.CreateMap<User, EditUserProfileViewModel>();
                ex.CreateMap<User, UserProfileViewModel>()
                    .ForMember(vm => vm.CountBooksInFavorite, options => options.MapFrom(u => u.FavoriteBooks.Count))
                    .ForMember(vm => vm.CountBooksInBasket, options => options.MapFrom(u => u.Basket.Books.Count));
                ex.CreateMap<User, UserPurchaseViewModel>()
                    .ForMember(vm => vm.PurchasesCount, options => options.MapFrom(u => u.Purchases.Count))
                    .ForMember(vm => vm.FullName, options => options.MapFrom(u => u.FirstName + " " + u.LastName));
                ex.CreateMap<EditUserMoneySpentBalanceBindingModel, User>();

                ex.CreateMap<BasketBook, CountBookInBasketViewModel>();

                ex.CreateMap<Basket, BasketViewModel>()
                    .ForMember(vm => vm.OwnerUserName, options => options.MapFrom(b => b.Owner.UserName))
                    .ForMember(vm => vm.OwnerAddress, options => options.MapFrom(b => b.Owner.Address))
                    .ForMember(vm => vm.OwnerFirstName, options => options.MapFrom(b => b.Owner.FirstName))
                    .ForMember(vm => vm.OwnerLastName, options => options.MapFrom(b => b.Owner.LastName))
                    .ForMember(vm => vm.OwnerPhoneNumber, options => options.MapFrom(b => b.Owner.PhoneNumber))
                    .ForMember(vm => vm.YouWillSave, options => options.MapFrom(b => (b.TotalPrice * b.Discount)/100))
                    .ForMember(vm => vm.LastPrice, options => options.MapFrom(b => (b.TotalPrice - (b.TotalPrice * b.Discount)/100)))
                    .ForMember(vm => vm.DeliveryDate, options => options.MapFrom(b => DateTime.Now.AddDays(2)));

                ex.CreateMap<Promotion, PromotionsViewModel>();
                ex.CreateMap<Promotion, PromotionViewModel>();
                ex.CreateMap<AddPromotionBindingModel, Promotion>();
                ex.CreateMap<Promotion, EditPromotionViewModel>();
                ex.CreateMap<EditPromotionBindingModel, Promotion>();

                ex.CreateMap<Book, HomeNewBookViewModel>();
                ex.CreateMap<Author, HomeNewBookAuthorViewModel>();
                ex.CreateMap<Promotion, HomePromotionViewModel>();

                ex.CreateMap<Purchase, AllPurchasesViewModel>();
                ex.CreateMap<Purchase, PurchaseDetailsViewModel>();
                ex.CreateMap<Purchase, EditPurchaseViewModel>();
                ex.CreateMap<EditPurchaseBindingModel, Purchase>();
                ex.CreateMap<Purchase, DeletePurchaseViewModel>();

                ex.CreateMap<Review, ReviewViewModel>();
                ex.CreateMap<AddReviewBindingModel, Review>();
            });
        }
    }
}
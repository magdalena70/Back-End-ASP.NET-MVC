﻿using AutoMapper;
using BookStore.Models.BindingModels.Author;
using BookStore.Models.BindingModels.Category;
using BookStore.Models.BindingModels.Promotion;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Author;
using BookStore.Models.ViewModels.Basket;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Category;
using BookStore.Models.ViewModels.Home;
using BookStore.Models.ViewModels.Promotion;
using BookStore.Models.ViewModels.User;
using System;

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

                ex.CreateMap<Category, AllCategoriesViewModel>();
                ex.CreateMap<Category, CategoryViewModel>();
                ex.CreateMap<AddCategoryBindingModel, Category>();
                ex.CreateMap<Category, EditCategoryViewModel>();
                ex.CreateMap<EditCategoryBindingModel, Category>();

                ex.CreateMap<User, UserFavoriteBooksViewModel>();
                ex.CreateMap<User, EditUserProfileViewModel>();
                ex.CreateMap<User, UserProfileViewModel>()
                    .ForMember(vm => vm.CountBooksInFavorite, options => options.MapFrom(u => u.FavoriteBooks.Count))
                    .ForMember(vm => vm.CountBooksInBasket, options => options.MapFrom(u => u.Basket.Books.Count));

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

            });
        }
    }
}
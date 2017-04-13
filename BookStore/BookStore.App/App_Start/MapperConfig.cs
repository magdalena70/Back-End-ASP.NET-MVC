using AutoMapper;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels;

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

                ex.CreateMap<Book, BookDetailsViewModel>();
                ex.CreateMap<Book, BooksViewModel>();
                ex.CreateMap<Book, CountBookInBasketViewModel>();


                ex.CreateMap<Category, AllCategoriesViewModel>();
                ex.CreateMap<Category, CategoryViewModel>();

                ex.CreateMap<User, UserFavoriteBooksViewModel>();
                ex.CreateMap<User, EditUserProfileViewModel>();
                ex.CreateMap<User, UserProfileViewModel>()
                    .ForMember(vm => vm.CountBooksInFavorite, options => options.MapFrom(u => u.FavoriteBooks.Count))
                    .ForMember(vm => vm.CountBooksInBasket, options => options.MapFrom(u => u.Basket.Books.Count));

                ex.CreateMap<Basket, BasketViewModel>()
                    .ForMember(vm => vm.OwnerUserName, options => options.MapFrom(b => b.Owner.UserName))
                    .ForMember(vm => vm.OwnerAddress, options => options.MapFrom(b => b.Owner.Address))
                    .ForMember(vm => vm.OwnerFirstName, options => options.MapFrom(b => b.Owner.FirstName))
                    .ForMember(vm => vm.OwnerLastName, options => options.MapFrom(b => b.Owner.LastName))
                    .ForMember(vm => vm.OwnerPhoneNumber, options => options.MapFrom(b => b.Owner.PhoneNumber));

                ex.CreateMap<Promotion, PromotionsViewModel>();

                ex.CreateMap<Book, HomeNewBookViewModel>();
                ex.CreateMap<Author, HomeNewBookAuthorViewModel>();
                ex.CreateMap<Promotion, HomePromotionViewModel>();

            });
        }
    }
}
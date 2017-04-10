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
                ex.CreateMap<Book, BookDetailsViewModel>();
                ex.CreateMap<Book, BooksViewModel>();
                ex.CreateMap<Author, AuthorWithBooksViewModel>()
                .ForMember(vm => vm.BooksCount, options => options.MapFrom(a => a.Books.Count));
                ex.CreateMap<Category, AllCategoriesViewModel>();
                ex.CreateMap<Category, CategoryViewModel>();
            });
        }
    }
}
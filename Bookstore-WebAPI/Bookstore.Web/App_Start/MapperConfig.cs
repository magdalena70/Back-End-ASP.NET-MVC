using AutoMapper;
using Bookstore.Models.BindingModels.Books;
using Bookstore.Models.EntityModels;
using Bookstore.Models.ViewModels.Books;
using System.Linq;

namespace Bookstore.Web.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(ex =>
            {
                ex.CreateMap<Book, BookDetailsViewModel>();
                ex.CreateMap<Book, BooksViewModel>();
                ex.CreateMap<Book, AllBooksViewModel>()
                    .ForMember(vm => 
                        vm.AuthorsFullName, options => 
                            options.MapFrom(b => b.Authors.Select(a => a.FullName)));
                ex.CreateMap<Book, AddBookViewModel>();
                ex.CreateMap<Book, EditBookViewModel>();
                ex.CreateMap<Book, DeleteBookViewModel>();

                ex.CreateMap<AddBookBindingModel, Book>();
                ex.CreateMap<EditBookBindingModel, Book>();
            });
        }
    }
}
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.App.Controllers;
using BookStore.Services.Interfaces;
using BookStore.Services;
using TestStack.FluentMVCTesting;
using BookStore.Models.ViewModels.Home;
using AutoMapper;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Author;

namespace BookStore.App.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _controller;
        private IHomeService _homeService;

        [TestInitialize]
        public void init()
        {
            this._homeService = new HomeService();
            this._controller = new HomeController(this._homeService);

            Mapper.Initialize(ex =>
            {
                ex.CreateMap<Promotion, HomePromotionViewModel>();
                ex.CreateMap<Author, HomeNewBookAuthorViewModel>();
                ex.CreateMap<Author, AuthorViewModel>();
                ex.CreateMap<Book, HomeNewBookViewModel>();
                ex.CreateMap<Book, BooksViewModel>();
            });

        }

        [TestMethod]
        public void Index_ShouldReturnHomePageViewModel()
        {
            this._controller.WithCallTo(hm => hm.Index())
                .ShouldRenderDefaultView()
                .WithModel<HomePageViewModel>();
        }

        [TestMethod]
        public void Index_ShouldReturn3BooksFromLastYear()
        {
            this._controller.WithCallTo(hm => hm.Index())
                .ShouldRenderDefaultView()
                .WithModel<HomePageViewModel>(m =>
                    m.Top3BooksFromLastYear.Count() == 3);
        }

        [TestMethod]
        public void Index_ShouldReturn3BooksFromThisYear()
        {
            this._controller.WithCallTo(hm => hm.Index())
                .ShouldRenderDefaultView()
                .WithModel<HomePageViewModel>(vm =>
                    vm.Top3BooksFromThisYear.Count() == 3);
        }

        [TestMethod]
        public void TermsAndConditions_ShouldReturnEmptyView()
        {
            this._controller.WithCallTo(hm => 
                hm.TermsAndConditions())
                    .ShouldRenderDefaultView();
        }
    }
}

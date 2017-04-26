using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Home;
using BookStore.Models.ViewModels.Book;

namespace BookStore.Services
{
    public class HomeService : Service
    {

        public HomePageViewModel GetHomePageViewModel()
        {
            var currPromotions = this.Context.Promotions
                .Where(p => p.StartDate <= DateTime.Now)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();
            var newBooks = this.Context.Books
                .Include("Authors")
                .OrderByDescending(b => b.IssueDate)
                .Take(9)
                .ToList();
            var top3FromLastYear = this.Context.Books
                .Include("Authors")
                .Where(b => b.IssueDate.Year == ((DateTime.Now.Year)-1))
                .OrderByDescending(b => b.Ratings.Sum(r => r.Value))
                .ThenBy(b => b.Baskets.Count)
                .ThenByDescending(p => p.Price)
                .Take(3)
                .ToList();
            var top3FromCurrentYear = this.Context.Books
                .Include("Authors")
                .Where(b => b.IssueDate.Year == (DateTime.Now.Year))
                .OrderByDescending(b => b.Ratings.Sum(r => r.Value))
                .ThenBy(b => b.Baskets.Count)
                .ThenByDescending(p => p.Price)
                .Take(3)
                .ToList();

            HomePageViewModel viewModel = new HomePageViewModel()
            {
                CurrentPromotions = Mapper.Map<IEnumerable<Promotion>, IEnumerable<HomePromotionViewModel>>(currPromotions),
                NewBooks = Mapper.Map<IEnumerable<Book>, IEnumerable<HomeNewBookViewModel>>(newBooks),
                Top3BooksFromThisYear = Mapper.Map<IEnumerable<Book>, IEnumerable<BooksViewModel>>(top3FromCurrentYear),
                Top3BooksFromLastYear = Mapper.Map<IEnumerable<Book>, IEnumerable<BooksViewModel>>(top3FromLastYear)
            };

            return viewModel;
        }
    }
}

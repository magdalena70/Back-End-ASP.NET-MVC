using System;
using System.Linq;
using AutoMapper;
using BookStore.Models.EntityModels;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Promotion;
using System.Net;

namespace BookStore.Services
{
    public class PromotionService : Service
    {

        public IEnumerable<PromotionsViewModel> GetAll()
        {
            var promotions = this.Context.Promotions
                .Include("Categories")
                .OrderByDescending(p => p.StartDate)
                .ThenByDescending(p => p.EndDate)
                .ThenBy(p => p.Name)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetCurrentAndUpcoming()
        {
            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.EndDate > DateTime.Now)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        private void AddPromotionStatus(IEnumerable<PromotionsViewModel> viewModel)
        {
            foreach (var promotion in viewModel)
            {
                if (promotion.EndDate < DateTime.Now)
                {
                    promotion.Status = "Expired";
                }
                else
                {
                    if (promotion.StartDate <= DateTime.Now)
                    {
                        promotion.Status = "Current";
                    }

                    if (promotion.StartDate > DateTime.Now)
                    {
                        promotion.Status = "Upcoming";
                    }
                }

            }
        }

        public PromotionsViewModel GetDetails(int? id)
        {
            Promotion promotion = this.Context.Promotions.Find(id);
            if (promotion == null)
            {
                return null;
            }

            PromotionsViewModel promotionsViewModel = Mapper.Map<Promotion, PromotionsViewModel>(promotion);
            promotionsViewModel.AreThereBooks = CheckIfThereAreBooks(promotion);
            return promotionsViewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDate(string startDateYear, string startDateMonth, string startDateDay)
        {
            startDateYear = WebUtility.HtmlDecode(startDateYear);
            startDateMonth = this.ValidateMonthAndDayValue(startDateMonth);
            startDateDay = this.ValidateMonthAndDayValue(startDateDay);

            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Year.ToString() == startDateYear &&
                            p.StartDate.Month.ToString() == startDateMonth &&
                            p.StartDate.Day.ToString() == startDateDay)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDateMonth(string startDateMonth)
        {
            startDateMonth = this.ValidateMonthAndDayValue(startDateMonth);

            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Month.ToString() == startDateMonth)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDateDay(string startDateDay)
        {
            startDateDay = this.ValidateMonthAndDayValue(startDateDay);


            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Day.ToString() == startDateDay)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDateYearAndStartDateDay(string startDateYear, string startDateDay)
        {
            startDateYear = WebUtility.HtmlDecode(startDateYear);
            startDateDay = this.ValidateMonthAndDayValue(startDateDay);


            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Year.ToString() == startDateYear &&
                            p.StartDate.Day.ToString() == startDateDay)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDateYear(string startDateYear)
        {
            startDateYear = WebUtility.HtmlDecode(startDateYear);

            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Year.ToString() == startDateYear)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDateYearAndStartDateMonth(string startDateYear, string startDateMonth)
        {
            startDateYear = WebUtility.HtmlDecode(startDateYear);
            startDateMonth = this.ValidateMonthAndDayValue(startDateMonth);

            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Year.ToString() == startDateYear &&
                            p.StartDate.Month.ToString() == startDateMonth)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        public IEnumerable<PromotionsViewModel> GetAllByStartDateMonthAndStartDateDay(string startDateDay, string startDateMonth)
        {
            startDateDay = this.ValidateMonthAndDayValue(startDateDay);
            startDateMonth = this.ValidateMonthAndDayValue(startDateMonth);

            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.StartDate.Day.ToString() == startDateDay &&
                            p.StartDate.Month.ToString() == startDateMonth)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);
            this.AddPromotionStatus(viewModel);

            return viewModel;
        }

        private string ValidateMonthAndDayValue(string value)
        {
            if (value.Length == 2 && value[0] == '0')
            {
                value = value[1].ToString();
            }

            //return WebUtility.HtmlDecode(value);
            return value;
        }

        private bool CheckIfThereAreBooks(Promotion promotion)
        {
            bool result;
            int countBooksInStock = 0;
            foreach (var category in promotion.Categories)
            {
                foreach (var book in category.Books)
                {
                    countBooksInStock += book.Quantity;
                }
            }

            if (countBooksInStock > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
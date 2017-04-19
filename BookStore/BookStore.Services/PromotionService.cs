using System;
using System.Linq;
using AutoMapper;
using BookStore.Models.EntityModels;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Promotion;

namespace BookStore.Services
{
    public class PromotionService : Service
    {

        public IEnumerable<PromotionsViewModel> GetAll()
        {
            var promotions = this.Context.Promotions
                .Include("Categories")
                .Where(p => p.EndDate > DateTime.Now)
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.EndDate)
                .ToList();

            IEnumerable<PromotionsViewModel> viewModel =
                Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionsViewModel>>(promotions);

            return viewModel;
        }

        public PromotionsViewModel GetDetails(int? id)
        {
            Promotion promotion = this.Context.Promotions.Find(id);
            if (promotion == null)
            {
                return null;
            }

            if (CheckIfThereAreBooks(promotion) == false)
            {
                return null;
            }

            PromotionsViewModel promotionsViewModel = Mapper.Map<Promotion, PromotionsViewModel>(promotion);
            return promotionsViewModel;
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

using System.Collections.Generic;
using BookStore.Models.BindingModels.Promotion;
using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Promotion;

namespace BookStore.Services.Interfaces
{
    public interface IPromotionService
    {
        void AddPromotion(AddPromotionBindingModel bindingModel);
        void DeletePromotion(int id);
        void EditPromotion(EditPromotionBindingModel bindingModel);
        IEnumerable<PromotionsViewModel> GetAll();
        IEnumerable<PromotionsViewModel> GetAllByStartDate(string startDateYear, string startDateMonth, string startDateDay);
        IEnumerable<PromotionsViewModel> GetAllByStartDateDay(string startDateDay);
        IEnumerable<PromotionsViewModel> GetAllByStartDateMonth(string startDateMonth);
        IEnumerable<PromotionsViewModel> GetAllByStartDateMonthAndStartDateDay(string startDateDay, string startDateMonth);
        IEnumerable<PromotionsViewModel> GetAllByStartDateYear(string startDateYear);
        IEnumerable<PromotionsViewModel> GetAllByStartDateYearAndStartDateDay(string startDateYear, string startDateDay);
        IEnumerable<PromotionsViewModel> GetAllByStartDateYearAndStartDateMonth(string startDateYear, string startDateMonth);
        IEnumerable<PromotionsViewModel> GetCurrentAndUpcoming();
        Promotion GetCurrentPromotion(int? id);
        PromotionsViewModel GetDetails(int? id);
        EditPromotionViewModel GetPromotionById(int? id);
        Promotion GetPromotionByName(string promotionName);
        string GetPromotionName(int id);
    }
}
using System.Web.Mvc;
using BookStore.Models.EntityModels;
using BookStore.App.Attributes;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Promotion;
using BookStore.Models.BindingModels.Promotion;
using System;
using BookStore.Services.Interfaces;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class PromotionsController : Controller
    {
        private IPromotionService promotionService;

        public PromotionsController(IPromotionService service)
        {
            this.promotionService = service;
        }

        // GET: Admin/Promotions?startDateYear=""&startDateMonth=""&startDateDay=""
        public ActionResult AllPromotions(string startDateYear, string startDateMonth, string startDateDay)
        { 
            IEnumerable<PromotionsViewModel> viewModel;

            if (string.IsNullOrEmpty(startDateYear) &&
                string.IsNullOrEmpty(startDateMonth) &&
                string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = "All promotions.";
                viewModel = this.promotionService.GetAll();
                return View(viewModel);

            }

            if (!string.IsNullOrEmpty(startDateYear) &&
                !string.IsNullOrEmpty(startDateMonth) &&
                !string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions with start date: {startDateYear}-{startDateMonth}-{startDateDay}";
                viewModel = this.promotionService.GetAllByStartDate(startDateYear, startDateMonth, startDateDay);
                return View(viewModel);
            }

            if(!string.IsNullOrEmpty(startDateYear) &&
                string.IsNullOrEmpty(startDateMonth) &&
                string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions launched during the specific year: { startDateYear}";
                viewModel = this.promotionService.GetAllByStartDateYear(startDateYear);
                return View(viewModel);
            }

            if (string.IsNullOrEmpty(startDateYear) &&
                !string.IsNullOrEmpty(startDateMonth) &&
                string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions launched during the specific month: { startDateMonth}";
                viewModel = this.promotionService.GetAllByStartDateMonth(startDateMonth);
                return View(viewModel);
            }

            if (string.IsNullOrEmpty(startDateYear) &&
                string.IsNullOrEmpty(startDateMonth) &&
                !string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions launched during the specific day: { startDateDay}";
                viewModel = this.promotionService.GetAllByStartDateDay(startDateDay);
                return View(viewModel);
            }

            if (!string.IsNullOrEmpty(startDateYear) &&
                !string.IsNullOrEmpty(startDateMonth) &&
                string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions launched during the {startDateMonth} month in year {startDateYear}";
                viewModel = this.promotionService.GetAllByStartDateYearAndStartDateMonth(startDateYear, startDateMonth);
                return View(viewModel);
            }

            if (string.IsNullOrEmpty(startDateYear) &&
                !string.IsNullOrEmpty(startDateMonth) &&
                !string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions launched during the {startDateDay} day in month {startDateMonth}";
                viewModel = this.promotionService.GetAllByStartDateMonthAndStartDateDay(startDateDay, startDateMonth);
                return View(viewModel);
            }

            if (!string.IsNullOrEmpty(startDateYear) &&
                string.IsNullOrEmpty(startDateMonth) &&
                !string.IsNullOrEmpty(startDateDay))
            {
                ViewBag.PromotionsTitle = $"Promotions launched during the {startDateDay} day in year {startDateYear}";
                viewModel = this.promotionService.GetAllByStartDateYearAndStartDateDay(startDateYear, startDateDay);
                return View(viewModel);
            }

            return View();
        }

        // GET: Admin/Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("Invalid URL - promotion's id can not be null");
            }

            PromotionsViewModel viewModel = this.promotionService.GetDetails(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no promotion with id {id}");
            }

            if (viewModel.AreThereBooks == false)
            {
                this.TempData["Info"] = "No books.";
            }

            return View(viewModel);
        }

        // GET: Admin/Promotions/AddPromotion
        public ActionResult AddPromotion()
        {
            AddPromotionViewModel viewModel = this.promotionService.GetAddPromotionViewModel();
            return View(viewModel);
        }

        // POST: Admin/Promotions/AddPromotion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPromotion([Bind(Include = "Id,Name,Text,StartDate,EndDate,Discount,Categories")] AddPromotionBindingModel bindingModel)
        {
            if (bindingModel.Discount < 1 || bindingModel.Discount > 100 )
            {
                return this.View(bindingModel);
            }

            if (ModelState.IsValid)
            {
                this.promotionService.AddPromotion(bindingModel);
                var newPromotion = this.promotionService.GetPromotionByName(bindingModel.Name);

                return RedirectToAction("Details", "Promotions", new { id = newPromotion.Id});
            }

            return View(bindingModel);
        }

        // GET: Admin/Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("Invalid URL - promotion's id can not be null");
            }

            EditPromotionViewModel viewModel = this.promotionService.GetPromotionById(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no promotion with id {id}");
            }

            return View(viewModel);
        }

        // POST: Admin/Promotions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Text,StartDate,EndDate,Discount")] EditPromotionBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.promotionService.EditPromotion(bindingModel);
                return RedirectToAction("Details", "Promotions", new { id = bindingModel.Id});
            }

            EditPromotionViewModel viewModel = this.promotionService.GetPromotionById(bindingModel.Id);
            return View(viewModel);
        }

        // GET: Admin/Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("Invalid URL - promotion's id can not be null");
            }

            Promotion promotion = this.promotionService.GetCurrentPromotion(id);
            if (promotion == null)
            {
                throw new Exception($"Invalid URL - there is no promotion with id {id}");
            }

            return View(promotion);
        }

        // POST: Admin/Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string promotionName = this.promotionService.GetPromotionName(id);
            this.promotionService.DeletePromotion(id);

            this.TempData["Success"] = $"Promotion '{promotionName}' was removed successfully.";
            return RedirectToAction("Allpromotions", "Promotions");
        }
    }
}

using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Collections.Generic;
using System;

namespace BookStore.App.Controllers
{
    [AllowAnonymous]
    public class PromotionsController : Controller
    {
        private PromotionService promotionService;

        public PromotionsController()
        {
            this.promotionService = new PromotionService();
        }

        // GET: Promotions
        public ActionResult AllPromotions()
        {
            IEnumerable<PromotionsViewModel> viewModel = this.promotionService.GetAll();
            return View(viewModel);
        }

        // GET: Promotions/Details/5
        [ActionName("Details")]
        public ActionResult PromotionDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            PromotionsViewModel viewModel = this.promotionService.GetDetails(id);
            if (viewModel == null)
            {
                this.TempData["Info"] = "No promotion.";
                return RedirectToAction("Index", "Home");
            }
            
            if (viewModel.EndDate < DateTime.Now)
            {
                this.TempData["Info"] = "Promotion expired.";
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}

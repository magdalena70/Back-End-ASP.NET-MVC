using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models.EntityModels;
using BookStore.App.Attributes;
using BookStore.Services;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Promotion;
using System;

namespace BookStore.App.Areas.Admin.Controllers
{
    [CustomAttributeAuth(Roles = "Admin")]
    public class PromotionsController : Controller
    {
        private BookStoreContext db = new BookStoreContext();
        private PromotionService promotionService;

        public PromotionsController()
        {
            this.promotionService = new PromotionService();
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PromotionsViewModel viewModel = this.promotionService.GetDetails(id);
            if (viewModel == null)
            {
                this.TempData["Info"] = "No promotion.";
                return RedirectToAction("AllPromotions", "Promotions");
            }

            if (viewModel.AreThereBooks == false)
            {
                this.TempData["Info"] = "No books.";
            }

            return View(viewModel);
        }

        // GET: Admin/Promotions/Create
        public ActionResult AddPromotion()
        {
            return View();
        }

        // POST: Admin/Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPromotion([Bind(Include = "Id,Name,Text,StartDate,EndDate,Discount")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotion);
        }

        // GET: Admin/Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Admin/Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Text,StartDate,EndDate,Discount")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Admin/Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Admin/Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            db.Promotions.Remove(promotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

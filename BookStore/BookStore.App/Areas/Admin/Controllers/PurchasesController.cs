using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models.EntityModels;
using BookStore.Services;
using BookStore.Models.ViewModels.Purchase;
using System.Collections.Generic;
using BookStore.Models.BindingModels.Purchase;

namespace BookStore.App.Areas.Admin.Controllers
{
    public class PurchasesController : Controller
    {
        private BookStoreContext db = new BookStoreContext();
        private PurchaseService purchaseService;

        public PurchasesController()
        {
            this.purchaseService = new PurchaseService();
        }

        // GET: Admin/Purchases
        public ActionResult AllPurchases()
        {
            IEnumerable<AllPurchasesViewModel> viewModel = this.purchaseService.GetAll();
            if (viewModel.Count() == 0)
            {
                this.TempData["Info"] = "No purchases.";
            }

            return View(viewModel);
        }

        // GET: Admin/Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PurchaseDetailsViewModel viewModel = this.purchaseService.GetDetails(id);
            if (viewModel == null)
            {
                return RedirectToAction("AllPurchases", "Purchases");
            }

            return View(viewModel);
        }

        // GET: Admin/Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditPurchaseViewModel viewModel = this.purchaseService.GetEditPurchaseViewModel(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Purchases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TotalPrice,Discount,CompletedOndate,IsCompleted,DeliveryAddress,DeliveryDate,DeliveryPrice")] EditPurchaseBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.purchaseService.EditPurchase(bindingModel);
                this.TempData["Success"] = $"Purchase with id {bindingModel.Id} was edited successfully.";
                return RedirectToAction("Details", "Purchases", new { id = bindingModel.Id});
            }

            return View(bindingModel);
        }

        // GET: Admin/Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Admin/Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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

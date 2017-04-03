using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNet.Identity;
using BookStore.Services;
using BookStore.Models.ViewModels;
using BookStore.Models.BindingModels;

namespace BookStore.App.Controllers
{
    [Authorize]
    public class BasketsController : Controller
    {
        private BookStoreContext context = new BookStoreContext();
        private BasketService basketService;

        public BasketsController()
        {
            this.basketService = new BasketService(context);
        }

        // GET: Baskets/Details
        public ActionResult Details()
        {
            var ownerId = User.Identity.GetUserId();
            BasketViewModel viewModel = this.basketService.GetBasketDetails(ownerId);
            if (viewModel == null)
            {
                this.TempData["Error"] = "You have no right to access.";
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        // POST: Baskets/AddToBasket
        [HttpPost, ActionName("AddToBasket")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBasketAndAddBookInIt([Bind(Include = "Id")] AddBookToBasketBindingModel book)
        {
            Book currBook = context.Books.Find(book.Id);
            User currUser = context.Users.Find(User.Identity.GetUserId());
            if (currBook.Quantity == 0)
            {
                this.TempData["Info"] = $"You can not add book '{currBook.Title}' in basket. it's not on stock.";
            }

            if (currBook.Quantity > 0)
            {
                this.basketService.AddBookToBasket(currUser, currBook);
                this.TempData["Success"] = $"You added book: '{currBook.Title}' in basket.";
            }

            return RedirectToAction("Details", "Baskets");
        }

        //// POST: Baskets/RemoveFromBasket
        //[HttpPost, ActionName("RemoveFromBasket")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Details([Bind(Include = "Id")] AddBookToBasketBindingModel book)
        //{
        //    var userId = User.Identity.GetUserId();
        //    Book currentBook = context.Books.Find(book.Id);
        //    Basket basket = context.Baskets.FirstOrDefault(b => b.Owner.Id == userId);

        //    if (currentBook.CountYourSelfInBasket > 1)
        //    {
        //        currentBook.CountYourSelfInBasket--;
        //        basket.TotalPrice -= currentBook.Price;
        //        basket.Owner = context.Users.Find(userId);
        //        context.SaveChanges();
        //        this.TempData["Success"] = $"You removed one book '{currentBook.Title}' from your basket.";
        //        return RedirectToAction("Details", "Baskets");
        //    }
           
        //    if (currentBook.CountYourSelfInBasket == 1)
        //    {
        //        currentBook.CountYourSelfInBasket = 0;
        //        basket.TotalPrice -= currentBook.Price;
        //        basket.Books.Remove(currentBook);
        //        basket.Owner = context.Users.Find(userId);
        //        context.SaveChanges();
        //    }

            
        //    this.TempData["Success"] = $"You removed book '{currentBook.Title}' from your basket.";
        //    return RedirectToAction("Details", "Baskets");
        //}

        // GET: Baskets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Basket basket = context.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }

            return View(basket);
        }

        // POST: Baskets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TotalPrice,Discount, Owner")] Basket basket)
        {
            basket.Owner = context.Users.First();
            // if (ModelState.IsValid)
            // {
            context.Entry(basket).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("UserProfile", "Users");
            // }
            // return View(basket);
        }

        // GET: Baskets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = context.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Basket basket = context.Baskets.Find(id);
            context.Baskets.Remove(basket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

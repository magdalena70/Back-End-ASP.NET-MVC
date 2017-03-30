using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNet.Identity;
using BookStore.Services;
using BookStore.Models.ViewModels;

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
        public ActionResult CreateBasketAndAddBookInIt([Bind(Include = "Id")] Book book)
        {
            User currUser = context.Users.Find(User.Identity.GetUserId());
            Basket currBasket = currUser.Basket;
            Book currBook = context.Books.Find(book.Id);

            if (currBasket == null)
            {
                Basket basket = new Basket()
                {
                    Owner = currUser,
                    TotalPrice = 0,
                    Discount = 0.0m
                };

                basket.Books.Add(currBook);
                basket.TotalPrice += currBook.Price;
                context.Baskets.Add(basket);
                context.SaveChanges();

                this.TempData["Success"] = $"You added book: {currBook.Title} in basket.";
                return RedirectToAction("UserProfile", "Users");
            }

            currBasket.Books.Add(currBook);
            currBasket.TotalPrice += currBook.Price;
            context.SaveChanges();

            this.TempData["Success"] = $"You added book: {currBook.Title} in basket.";
            return RedirectToAction("UserProfile", "Users");
        }

        // POST: Baskets/RemoveFromBasket
        [HttpPost, ActionName("RemoveFromBasket")]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Id")] Book book)
        {
            var userId = User.Identity.GetUserId();
            Book currentBook = context.Books.Find(book.Id);
            Basket basket = context.Baskets.FirstOrDefault(b => b.Owner.Id == userId);
            basket.TotalPrice -= currentBook.Price;
            basket.Books.Remove(currentBook);
            basket.Owner = context.Users.First();

            context.SaveChanges();
            this.TempData["Success"] = $"You removed one book from your basket.";
            return RedirectToAction("Details", "Baskets");
        }

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

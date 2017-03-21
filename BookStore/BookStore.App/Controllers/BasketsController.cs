using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNet.Identity;

namespace BookStore.App.Controllers
{
    public class BasketsController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Baskets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Basket basket = db.Baskets.Find(id);

            if (basket == null)
            {
                return HttpNotFound();
            }

            return View(basket);
        }

        // POST: Baskets/AddToBasket
        [HttpPost, ActionName("AddToBasket")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBasketAndAddBookInIt([Bind(Include = "Id")] Book book)
        {
            User currUser = db.Users.Find(User.Identity.GetUserId());
            Basket currBasket = currUser.Basket;
            Book currBook = db.Books.Find(book.Id);

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
                db.Baskets.Add(basket);
                db.SaveChanges();
                return RedirectToAction("UserProfile", "Users");
            }

            currBasket.Books.Add(currBook);
            currBasket.TotalPrice += currBook.Price;
            db.SaveChanges();

            return RedirectToAction("UserProfile", "Users");
        }

        // POST: Baskets/RemoveFromBasket
        [HttpPost, ActionName("RemoveFromBasket")]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Id")] Book book)
        {
            var userId = User.Identity.GetUserId();
            Book currentBook = db.Books.Find(book.Id);
            Basket basket = db.Baskets.FirstOrDefault(b => b.Owner.Id == userId);
            basket.TotalPrice -= currentBook.Price;
            basket.Books.Remove(currentBook);
            basket.Owner = db.Users.First();

            db.SaveChanges();
            return RedirectToAction("Details", "Baskets", new { id = basket.Id });
        }

        // GET: Baskets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Basket basket = db.Baskets.Find(id);
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
            basket.Owner = db.Users.First();
            // if (ModelState.IsValid)
            // {
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();
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
            Basket basket = db.Baskets.Find(id);
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
            Basket basket = db.Baskets.Find(id);
            db.Baskets.Remove(basket);
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

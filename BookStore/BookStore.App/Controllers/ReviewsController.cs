using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models.EntityModels;
using BookStore.Services;
using Microsoft.AspNet.Identity;
using System;
using BookStore.Models.BindingModels.Review;
using AutoMapper;
using BookStore.Models.ViewModels.Review;

namespace BookStore.App.Controllers
{
    public class ReviewsController : Controller
    {
        private BookStoreContext db = new BookStoreContext();
        private ReviewService reviewService;

        public ReviewsController()
        {
            this.reviewService = new ReviewService();
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(AddReviewBindingModel bindingModel, int id)
        {
            if (bindingModel != null && ModelState.IsValid)
            {
                var newReview = new Review()
                {
                    Author = db.Users.Find(User.Identity.GetUserId()),
                    Book = db.Books.Find(id),
                    DateCreate = DateTime.Now,
                    Text = bindingModel.Text
                };
                db.Reviews.Add(newReview);
                db.SaveChanges();

                var newReviewDb = db.Reviews.Find(newReview.Id);
                var viewModel = Mapper.Map<Review, ReviewViewModel>(newReviewDb);
                return this.PartialView("DisplayTemplates/ReviewViewModel", viewModel);
            }

            return Json("Error");
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,DateCreate")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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

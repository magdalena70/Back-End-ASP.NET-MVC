using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MicroBlogWeb.Data;
using MicroBlogWeb.Models;
using Microsoft.AspNet.Identity;

namespace MicroBlogWeb.App.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private MicroBlogWebContext db = new MicroBlogWebContext();

        // GET: Messages
        public ActionResult ReceivedMessages()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            var messages = db.Messages
                .Where(m => m.Recipient.Id == currentUser.Id)
                .OrderByDescending(m => m.Id)
                .ToList();

            return View(messages);
        }

        // GET: Messages
        public ActionResult SentMessages()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            var messages = db.Messages
                .Where(m => m.Author.Id == currentUser.Id)
                .OrderByDescending(m => m.Id)
                .ToList();

            return View(messages);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            if (message.Recipient.Id == User.Identity.GetUserId())
            {
                if (message.IsActive == true)
                {
                    message.IsActive = false;
                    db.SaveChanges();
                }
            }
            else if (message.Author.Id != User.Identity.GetUserId())
            {
                this.TempData["Error"] = "You can see only received messages or sent by you.";
                return RedirectToAction("Index", "Home");
            }

            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create(string id)
        {
            var author = db.Users.Find(User.Identity.GetUserId());
            var recipient = db.Users.Find(id);
            if (author.Id == recipient.Id)
            {
                this.TempData["Error"] = "You can not send message to your self";
                return RedirectToAction("Index", "Tweets");
            }
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text")] Message message, string id)
        {
            var author = db.Users.Find(User.Identity.GetUserId());
            var recipient = db.Users.Find(id);
         
            message.Author = author;
            message.Recipient = recipient;
            message.IsActive = true;

            if (ModelState.IsValid)
            {
            db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index", "Tweets");
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            if (message.Author.Id != User.Identity.GetUserId())
            {
                this.TempData["Error"] = "You are not author of this message. You can not edit it.";
                return RedirectToAction("Index", "Tweets");
            }

            return View(message);
        }

        // POST: Messages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();

                this.TempData["Message"] = "This message was edited successfuly.";
                return RedirectToAction("Details/" + message.Id, "Messages");

            }

            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            if (message.Author.Id != User.Identity.GetUserId())
            {
                this.TempData["Error"] = "You are not author of this message. You can not remove it.";
                return RedirectToAction("Index", "Tweets");
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();

            this.TempData["Message"] = "This message was removed successfuly.";
            return RedirectToAction("Index", "Tweets");
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

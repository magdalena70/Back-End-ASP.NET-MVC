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
    public class UsersController : Controller
    {
        private MicroBlogWebContext db = new MicroBlogWebContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Home/5
        public ActionResult Home(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Profile/5
        public ActionResult Profile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            int count = 0;
            if (user.ReceivedMessages.Count > 0)
            {
                foreach (var msg in user.ReceivedMessages)
                {
                    if (msg.IsActive == true)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    this.TempData["Info"] = $"You have {count} new messages.";
                    return RedirectToAction("Index", "Messages");
                }
            }
            return View(user);
        }

        // POST: Users/Profile/5
        [HttpPost, ActionName("Profile")]
        [ValidateAntiForgeryToken]
        public ActionResult FollowUser(string id)
        {
            User selectedUser = db.Users.Find(id);
            string followerId = User.Identity.GetUserId();
            User follower = db.Users.Find(followerId);
            if (selectedUser.Id == follower.Id)
            {
                this.TempData["Error"] = "You can not follow your self.";
                return View(selectedUser);
            }

            selectedUser.Followers.Add(follower);
            follower.Following.Add(selectedUser);
            db.SaveChanges();
            return RedirectToAction("Index", "Tweets");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,Email,PhoneNumber,UserName")] User user)
        {
            if (ModelState.IsValid)
            {      
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.Id != User.Identity.GetUserId())
            {
                this.TempData["Error"] = "You can edit only your oun profile.";
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Profile/" + user.Id, "Users");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.Id != User.Identity.GetUserId())
            {
                this.TempData["Error"] = "You can not remove any user.";
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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


namespace Twitter.Web.Controllers
{
    using System;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Twitter.Data;
    using Twitter.Models;
    using Twitter.Web.ViewModels;

    [Authorize]
    public class MessagesController: BaseController
    {
        public MessagesController(ITwitterData data)
            :base(data)
        {
        }
        
        public ActionResult SendMessage(string id, Message message)
        {
            if (ModelState.IsValid)
            {
                this.Data.Messages.Add(new Message
                {
                    Title = message.Title,
                    Content = message.Content,
                    SentToDate = DateTime.Now,
                    SenderName = this.UserProfile.UserName,
                    RecipientId = id
                });
                this.Data.SaveChanges();

                return this.RedirectToAction("NewMessageDetails");
            }

            return this.View();
        }

        public ActionResult NewMessageDetails()
        {
            var newMessage = this.Data.Messages.All()
                .Where(m => m.SenderName == this.UserProfile.UserName)
                .OrderByDescending(m => m.SentToDate)
                .FirstOrDefault();
            if (newMessage == null)
            {
                return this.HttpNotFound();
            }

            return this.View(newMessage);
        }

        //to do
        public ActionResult EditMessage()
        {
            return this.View();
        }

        public ActionResult DeleteMessage(int id)
        {
            var message = this.Data.Messages.Find(id);
            if (message == null)
            {
                return this.HttpNotFound();
            }

            return this.View(message);
        }

        //to do
        public ActionResult Delete(int id)
        {
            var message = this.Data.Messages.Find(id);
            if (message == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Tweets.Delete(message);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
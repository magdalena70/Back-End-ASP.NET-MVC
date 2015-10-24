
namespace Twitter.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Twitter.Data;
    using Twitter.Web.ViewModels;
    using Twitter.Models;

    public class NotificationsController : BaseController
    {
        public NotificationsController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult DeleteNotification(int id)
        {
            var notification = this.Data.Notifications.Find(id);
            if (notification == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Notifications.Delete(notification);
            this.Data.SaveChanges();

            return this.View();
        }
    }
}
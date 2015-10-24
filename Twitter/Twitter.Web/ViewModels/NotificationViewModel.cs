
namespace Twitter.Web.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using Twitter.Models;

    public class NotificationViewModel
    {
        public static Expression<Func<UserNotification, NotificationViewModel>> Create
        {
            get
            {
                return un => new NotificationViewModel
                {
                    Id = un.Notification.Id,
                    Type = un.Notification.Type,
                    Content = un.Notification.Content,
                    SentToDate = un.SentToDate,
                    UserNotificationId = un.Id
                };
            }
        }

        public int Id { get; set; }

        public NotificationType Type { get; set; }

        public string Content { get; set; }

        public DateTime SentToDate { get; set; }

        public int UserNotificationId { get; set; }
    }
}
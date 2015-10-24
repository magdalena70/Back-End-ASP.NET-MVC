
namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserNotification
    {
        public int Id { get; set; }

        public DateTime SentToDate { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }
    }
}

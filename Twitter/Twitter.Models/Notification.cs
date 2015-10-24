
namespace Twitter.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        private ICollection<UserNotification> users;

        public Notification()
        {
            this.users = new HashSet<UserNotification>();
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        public virtual ICollection<UserNotification> UserNotifications
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}

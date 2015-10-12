
namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserNotification
    {
        public int Id { get; set; }

        [Required]
        public string FanId { get; set; }

        public virtual User Fan { get; set; }

        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }
    }
}

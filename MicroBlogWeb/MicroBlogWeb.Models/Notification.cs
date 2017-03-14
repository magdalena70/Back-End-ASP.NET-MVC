using System;
using System.ComponentModel.DataAnnotations;

namespace MicroBlogWeb.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        [Required]
        public virtual User Recipient { get; set; }
    }
}

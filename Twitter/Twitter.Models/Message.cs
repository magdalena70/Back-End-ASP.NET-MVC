
namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

     public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentToDate { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string RecipientId { get; set; }

        public virtual User Recipient { get; set; }
    }
}

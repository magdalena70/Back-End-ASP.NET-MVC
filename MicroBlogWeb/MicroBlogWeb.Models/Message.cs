using System.ComponentModel.DataAnnotations;

namespace MicroBlogWeb.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsActive { get; set; }

        public virtual User Author { get; set; }

        public virtual User Recipient { get; set; }
    }
}

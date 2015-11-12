
namespace Snippy.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual User Author { get; set; }

        public virtual Snippet Snippet { get; set; }
    }
}

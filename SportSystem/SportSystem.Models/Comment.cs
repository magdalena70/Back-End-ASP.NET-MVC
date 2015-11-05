namespace SportSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public virtual Match Match { get; set; }
    }
}
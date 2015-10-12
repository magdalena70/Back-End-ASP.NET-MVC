
namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}

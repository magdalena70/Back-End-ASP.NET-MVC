
namespace Snippy.Models
{
    using System;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual User Author { get; set; }

        public virtual Snippet Snippet { get; set; }
    }
}

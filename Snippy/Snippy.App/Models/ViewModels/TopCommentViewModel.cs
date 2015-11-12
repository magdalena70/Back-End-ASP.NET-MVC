using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.ViewModels
{
    public class TopCommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public string Author { get; set; }

        public string SnippetTitle { get; set; }

        public int SnippetId { get; set; }
    }
}
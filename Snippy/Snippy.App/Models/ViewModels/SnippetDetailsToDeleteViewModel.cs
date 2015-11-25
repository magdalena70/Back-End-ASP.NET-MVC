﻿
namespace Snippy.App.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class SnippetDetailsToDeleteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public string Language { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
﻿
namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class UserSnippetViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public int LanguageId { get; set; }

        public IEnumerable<LabelViewModel> Labels { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<SnippetViewModel> FiveLatestSnippets { get; set; }

        public IEnumerable<CommentViewModel> FiveLatestComments { get; set; }

        public IEnumerable<LabelViewModel> FiveBestLabels { get; set; }
    }
}

namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<SnippetViewModel> FiveLatestSnippets { get; set; }

        public IEnumerable<TopCommentViewModel> FiveLatestComments { get; set; }

        public IEnumerable<LabelViewModel> FiveBestLabels { get; set; }
    }
}
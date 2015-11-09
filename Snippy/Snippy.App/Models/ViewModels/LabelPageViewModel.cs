
namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class LabelPageViewModel
    {
        public LabelSnippetViewModel Label { get; set; }

        public IEnumerable<SnippetsByLabelViewModel> Snippets { get; set; }
    }
}

namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class LanguagePageViewModel
    {
        public LanguageSnippetViewModel Language { get; set; }

        public IEnumerable<SnippetsByLanguageViewModel> Snippets { get; set; }
    }
}
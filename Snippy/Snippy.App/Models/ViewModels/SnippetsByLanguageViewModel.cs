
namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class SnippetsByLanguageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public IEnumerable<LabelSnippetViewModel> Labels { get; set; }
    }
}
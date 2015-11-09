
namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class UserPageViewModel
    {
        public UserDetailsViewModel UserDetails { get; set; }

        public IEnumerable<UserSnippetViewModel> UserSnippets { get; set; }
    }
}
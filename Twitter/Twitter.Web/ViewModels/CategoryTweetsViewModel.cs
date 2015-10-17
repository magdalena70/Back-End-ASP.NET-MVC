
namespace Twitter.Web.ViewModels
{
    using System.Collections.Generic;
    public class CategoryTweetsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TweetViewModel> Tweets { get; set; }
    }
}
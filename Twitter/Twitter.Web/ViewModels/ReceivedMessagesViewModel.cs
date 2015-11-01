
namespace Twitter.Web.ViewModels
{
    using Antlr.Runtime.Misc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class ReceivedMessagesViewModel
    {
        public int Id { get; set; }

        public string SentToDate { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SenderName { get; set; }
    }
}
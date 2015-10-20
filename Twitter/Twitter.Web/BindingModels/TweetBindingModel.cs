
namespace Twitter.Web.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TweetBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime SentToDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
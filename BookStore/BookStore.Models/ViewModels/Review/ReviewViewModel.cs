using BookStore.Models.ViewModels.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateCreate { get; set; }

        public AllUsersViewModel Author { get; set; }

        //public Book Book { get; set; }
    }
}

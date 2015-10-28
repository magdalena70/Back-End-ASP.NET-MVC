
namespace Twitter.Web.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class RetweetViewModel
    {
        public static Expression<Func<Retweet, RetweetViewModel>> Create
        {
            get
            {
                return r => new RetweetViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Details = r.Details,
                    SentToDate = r.SentToDate.ToString(),
                    Retweeter = r.Retweeter.UserName,
                    RetweeterAvatar = r.Retweeter.AvatarUrl != null ?
                        r.Retweeter.AvatarUrl :
                        "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR_N1xlcGHULtzO3ylNBd0MPc65_e4_L2OcKH_okfIm9HUN3R8i",
                    ImageUrl = r.ImageUrl != null ?
                        r.ImageUrl :
                        "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcR3_DG29P5r1_2LkRvHwhGeUlypRQl6HxLsi02BCF4e2EoCOQt6"
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string SentToDate { get; set; }

        public string Retweeter { get; set; }

        public string ImageUrl { get; set; }

        public string RetweeterAvatar { get; set; }
    }
}
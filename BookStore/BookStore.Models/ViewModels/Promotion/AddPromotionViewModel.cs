using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.Models.ViewModels.Promotion
{
    public class AddPromotionViewModel
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Discount { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }
    }
}

using BookStore.Models.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Promotion
{
    public class PromotionsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00} %", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }

        public bool AreThereBooks { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}

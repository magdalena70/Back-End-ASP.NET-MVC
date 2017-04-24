using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.BindingModels.Promotion
{
    public class AddPromotionBindingModel
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
    }
}

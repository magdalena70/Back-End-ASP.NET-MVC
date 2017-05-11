using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.EntityModels
{
    public class Promotion
    {
        public Promotion()
        {
            this.Categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.EntityModels
{
    public class Purchase
    {
        public Purchase()
        {
            this.Books = new HashSet<BasketBook>();
        }

        [Key]
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public DateTime CompletedOndate { get; set; }

        public bool IsCompleted { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; }

        public decimal DeliveryPrice { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<BasketBook> Books { get; set; }
    }
}

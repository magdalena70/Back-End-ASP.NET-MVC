﻿using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.EntityModels
{
    public class BasketBook
    {
        [Key]
        public int Id { get; set; }

        public virtual Basket Basket { get; set; }

        public virtual Book Book { get; set; }
    }
}

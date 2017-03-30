using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string OwnerUserName { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Discount { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}

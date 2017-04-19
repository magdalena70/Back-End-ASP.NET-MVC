using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Promotion;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BooksViewModel> Books { get; set; }

        public int BooksCount { get; set; }

        public ICollection<PromotionViewModel> Promotions { get; set; }
    }
}

namespace Bookstore.Data
{
    using Bookstore.Models.EntityModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class BookstoreDbContext : IdentityDbContext<User>
    {
        public BookstoreDbContext()
            : base("name=BookstoreDbContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Basket> Baskets { get; set; }

        public virtual DbSet<BasketBook> BasketsBooks { get; set; }

        public virtual DbSet<Promotion> Promotions { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public static BookstoreDbContext Create()
        {
            return new BookstoreDbContext();
        }
    }

}
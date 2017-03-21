namespace BookStore.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class BookStoreContext : IdentityDbContext<User>
    {
        public BookStoreContext()
            : base("BookStoreContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Basket> Baskets { get; set; }

        public static BookStoreContext Create()
        {
            return new BookStoreContext();
        }
    }
}
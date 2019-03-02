using Bookstore.Data;

namespace Bookstore.Services
{
    public abstract class Service
    {

        public Service()
        {
            this.Context = new BookstoreDbContext();
        }

        protected BookstoreDbContext Context { get; }
    }
}

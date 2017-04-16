using BookStore.Data;

namespace BookStore.Services
{
    public abstract class Service
    {

        public Service()
        {
            this.Context = new BookStoreContext();
        }

        protected BookStoreContext Context { get; }
    }
}

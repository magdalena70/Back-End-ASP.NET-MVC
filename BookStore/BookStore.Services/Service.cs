using BookStore.Data;

namespace BookStore.Services
{
    public abstract class Service
    {
        protected BookStoreContext context;

        public Service(BookStoreContext context)
        {
            this.context = context;
        }
    }
}

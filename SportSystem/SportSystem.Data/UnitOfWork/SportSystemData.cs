
namespace SportSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Repositories;
    using Models;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class SportSystemData : ISportSystemData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        private IUserStore<User> userStore;

        public SportSystemData()
            :this(new SportSystemDbContext())
        {
        }

        public SportSystemData(DbContext context)
        {
            this.dbContext = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Bet> Bets
        {
            get
            {
                return this.GetRepository<Bet>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Match> Matches
        {
            get
            {
                return this.GetRepository<Match>();
            }
        }

        public IRepository<Player> Players
        {
            get
            {
                return this.GetRepository<Player>();
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                return this.GetRepository<Team>();
            }
        }

        public IRepository<Vote> Votes
        {
            get
            {
                return this.GetRepository<Vote>();
            }
        }

        public IUserStore<User> UserStore
        {
            get
            {
                if (this.userStore == null)
                {
                    this.userStore = new UserStore<User>(this.dbContext);
                }

                return this.userStore;
            }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericEfRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}


namespace Twitter.Data
{
    using System;
    using System.Collections.Generic;
    using Twitter.Data.Repositories;
    using Twitter.Models;

    public class TwitterData : ITwitterData
    {
        private ITwitterDbContext context;
        private IDictionary<Type, object> repositories;

        public TwitterData(ITwitterDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
            /*set
            {
                throw new NotImplementedException();
            }*/
        }

        public IRepository<AdministrationLog> AdministrationLogs
        {
            get
            {
                return this.GetRepository<AdministrationLog>();
            }
           /* set
            {
                throw new NotImplementedException();
            }*/
        }

        public IRepository<Group> Groups
        {
            get
            {
                return this.GetRepository<Group>();
            }
            /*set
            {
                throw new NotImplementedException();
            }*/
        }

        public IRepository<Tweet> Tweets
        {
            get
            {
                return this.GetRepository<Tweet>();
            }
            /*set
            {
                throw new NotImplementedException();
            }*/
        }

        public IRepository<UserFavouriteTweet> FavoriteTweets
        {
            get
            {
                return this.GetRepository<UserFavouriteTweet>();
            }
            /*set
            {
                throw new NotImplementedException();
            }*/
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
            /*set
            {
                throw new NotImplementedException();
            }*/
        }

        public IRepository<Notification> Notifications
        {
            get
            {
                return this.GetRepository<Notification>();
            }
            /*set
            {
                throw new NotImplementedException();
            }*/
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
                //can add custom repositories here
                var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}

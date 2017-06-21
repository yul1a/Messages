using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToDB;

namespace Messages.Server
{
    public class Storage
    {
        private readonly Linq2Db host;

        public Storage(Linq2Db host)
        {
            this.host = host;
        }

        public IQueryable<T> Select<T>() where T : class
        {
            return host.GetConnection().GetTable<T>();
        }

        public void Create<T>(T item) where T : class
        {
            host.GetConnection().Insert(item);
        }


        public void Remove<T>(Expression<Func<T, bool>> filter) where T : class
        {
            host.GetConnection().GetTable<T>().Delete(filter);
        }

        public void Update<T>(T item) where T : class
        {
            host.GetConnection().Update(item);
        }
    }
}
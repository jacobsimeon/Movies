namespace Movies.DataAccess
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using NHibernate;
    using NHibernate.Linq;

    public interface IRepository<T> where T : class
    {
        void Save(T obj);
        T Find(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly ISession _Session;

        public Repository(ISession session)
        {
            _Session = session;
        }

        public void Save(T obj)
        {
            _Session.Save(obj);
        }

        public T Find(int id)
        {
            return _Session.Get<T>(id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _Session.Query<T>().Where(predicate);
        }


    }
}
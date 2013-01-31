namespace Movies.DataAccess
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using NHibernate;
    using NHibernate.Linq;

    public interface IPrimaryKeyable
    {
        int Id { get; set; }
    }

    public interface IRepository<T>
        where T : IPrimaryKeyable 
    {
        void Save(T obj);
        T Find(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        void Delete(T obj);
    }

    public class Repository<T> : IRepository<T>
        where T : IPrimaryKeyable 
    {
        private readonly ISession _Session;

        public Repository(ISession session)
        {
            _Session = session;
        }

        public void Save(T obj)
        {
            if (obj.Id > 0)
            {
                _Session.Update(obj);
                _Session.Flush();
            }
            else
            {
                _Session.Save(obj);
            }
        }

        public T Find(int id)
        {
            return _Session.Get<T>(id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _Session.Query<T>().Where(predicate);
        }

        public void Delete(T obj)
        {
            _Session.Delete(obj);
            _Session.Flush();
        }


    }
}
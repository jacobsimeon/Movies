namespace Movies.DataAccess
{
    using Movies.Models;

    using NHibernate;

    public interface IDataContext
    {
        IRepository<User> Users { get; }
    }

    public class DataContext : IDataContext
    {
        private readonly ISession _Session;

        public DataContext(ISession session)
        {
            _Session = session;
        }

        private IRepository<User> _Users;

        public IRepository<User> Users
        {
            get
            {
                return _Users = _Users ?? new Repository<User>(_Session);
            }
        }
    }
}
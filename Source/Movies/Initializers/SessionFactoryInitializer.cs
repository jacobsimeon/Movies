namespace Movies.Initializers
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;

    public class SessionFactoryInitializer
	{
		public static ISessionFactory Initialize(string connectionString)
		{
            var config = SQLiteConfiguration.Standard.ConnectionString(connectionString);
            return Fluently.Configure().Database(config).BuildSessionFactory();
		}
	}
}

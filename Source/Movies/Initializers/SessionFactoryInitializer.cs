namespace Movies.Initializers
{
    using System.Data.SQLite;
    using System.IO;
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;

    public class SessionFactoryInitializer
	{
		public static ISessionFactory Initialize(string connectionString)
		{
            var assembly = Assembly.GetExecutingAssembly();
            var location = Assembly.GetExecutingAssembly().Location;
            var dir = Path.GetDirectoryName(location);
            var sqlFile = Path.Combine(dir, "mmdb.sqlite3");

            if (!File.Exists(sqlFile))
            {
                var sqlite = new SQLiteConnection(connectionString);
                sqlite.Open();
                var cmd = sqlite.CreateCommand();
                var stream = assembly.GetManifestResourceStream("Movies.Database.Schema.sql");
                var reader = new StreamReader(stream);
                var sql = reader.ReadToEnd();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sqlite.Close();
            }

            var config = SQLiteConfiguration.Standard.ConnectionString(connectionString);
            return Fluently.Configure().Database(config).CurrentSessionContext("web").BuildSessionFactory();
		}
	}
}

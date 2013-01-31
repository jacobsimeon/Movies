namespace Movies.Initializers
{
    using System;
    using System.Data;
    using System.IO;
    using System.Reflection;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Movies.Mappings;
    using Movies.Models;

    using NHibernate;

    public class MoviesAutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "Movies.Models";
        }
    }

    public class SessionFactoryInitializer
	{
		public static ISessionFactory Initialize(string connectionString)
		{
            var mapConfig = new MoviesAutoMappingConfiguration();
            var config = SQLiteConfiguration.Standard.ConnectionString(connectionString);

            var factory = Fluently
                .Configure()
                .Database(config)
                .Mappings(mappings => 
                    mappings.AutoMappings
                        .Add(AutoMap.AssemblyOf<User>(mapConfig)
                        .UseOverridesFromAssemblyOf<UserMap>))
                .CurrentSessionContext("web")
                .BuildSessionFactory();

            InitializeSchema(factory.OpenSession().Connection);

            return factory;
		}

        public static void InitializeSchema(IDbConnection connection)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var cmd = connection.CreateCommand();
            var stream = assembly.GetManifestResourceStream("Movies.Database.Schema.sql");
            var reader = new StreamReader(stream);
            var sql = reader.ReadToEnd();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
	}
}

namespace Movies.Initializers
{
    using System.Configuration;

    using FluentValidation;

    using Movies.DataAccess;
    using Movies.Models;
    using Movies.Validators;

    using NHibernate;

    using StructureMap;

    public class IocInitializer
	{
		public static void InitializeObjectFactory()
		{
			var connectionString = ConfigurationManager.ConnectionStrings["Mmdb"].ConnectionString;

			ObjectFactory.Initialize(ioc => {
				ioc.For<ISessionFactory>()
                    .Singleton()
                    .Use(SessionFactoryInitializer.Initialize(connectionString));

				ioc.For<ISession>()
                    .HttpContextScoped()
                    .Use(context => context.GetInstance<ISessionFactory>().OpenSession());

                ioc.For<IValidator<User>>().Use<UserValidator>();

                ioc.For<IDataContext>().Use<DataContext>();
			});
		}
	}
}

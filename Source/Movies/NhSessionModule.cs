namespace Movies
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using NHibernate;
    using NHibernate.Context;

    using StructureMap;

    public class NhSessionModule : IHttpModule
	{
		public ISession GetSession()
		{
			return ObjectFactory.GetInstance<ISession>();
		}

		public ISessionFactory GetSessionFactory()
		{
			return ObjectFactory.GetInstance<ISessionFactory>();
		}

		public void Init(HttpApplication context)
		{
			context.BeginRequest += (c, args) => RunIfApplicationRequest(c, InitializeSession);
			context.EndRequest += (c, args) => RunIfApplicationRequest(c, CleanupSession);
		}

		private bool IsApplicationPath(string requestPath)
		{
			var isStaticFile = Regex.IsMatch(requestPath, @".*\.(css|js|gif|png|jpg|ico)(/.*)?");
			return !isStaticFile;
		}

		private void RunIfApplicationRequest(object context, Action<MvcApplication> command)
		{
			var app = context as MvcApplication;
			var path = app != null ? app.Request.Path : "";
			if(IsApplicationPath(path))
			{
				command(app);
			}
		}

		private void InitializeSession(MvcApplication app)
		{
			var session = GetSession();
			session.BeginTransaction();
			CurrentSessionContext.Bind(session);
		}

		private void CleanupSession(MvcApplication app)
		{
			var session = CurrentSessionContext.Unbind(GetSessionFactory());
			if(session == null)
			{
				return;
			}

            try
            {
                session.Transaction.Commit();
            }
            catch (Exception e)
            {
                session.Transaction.Rollback();
            	throw new Exception("Unable to commit transaction", e);
            }
            finally
            {
				ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            }
		}

		public void Dispose()
		{ }
	}

	public static class NhSessionModuleRegistrar
	{
		public static void Register()
		{
			DynamicModuleUtility.RegisterModule(typeof(NhSessionModule));
		}
	}
}

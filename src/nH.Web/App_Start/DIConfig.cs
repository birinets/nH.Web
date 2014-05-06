using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using LightInject;

using nH.Data.Models;
using nH.Data.Repositories;
using nH.Web.Models;

namespace nH.Web
{
	public class DiConfig
	{
		public static void Register(ServiceContainer container, HttpConfiguration config)
		{
			container.RegisterApiControllers();

			container.Register<ICacheContext, CacheContext>(new PerContainerLifetime());
			container.Register<DbContext>(f => new NuGetDataContext(GetConnectionString()),
				new PerContainerLifetime());
			container.Register<IDataRepository<Repository>, DataRepository<Repository, NuGetDataContext>>();
			container.Register<IDataRepository<Session>, DataRepository<Session, NuGetDataContext>>();
			container.Register<IDataRepository<LogEntry>, DataRepository<LogEntry, NuGetDataContext>>();

			container.EnablePerWebRequestScope();
			container.EnableWebApi(config);
		}

		private static string GetConnectionString()
		{
			var uriString = ConfigurationManager.AppSettings["SQLSERVER_URI"];
			if (uriString == null)
			{
				return "Name=DefaultConnection";
			}

			var uri = new Uri(uriString);
			var connectionString = new SqlConnectionStringBuilder
			{
				DataSource = uri.Host,
				InitialCatalog = uri.AbsolutePath.Trim('/'),
				UserID = uri.UserInfo.Split(':').First(),
				Password = uri.UserInfo.Split(':').Last(),
				MultipleActiveResultSets = true
			}.ConnectionString;

			Trace.TraceInformation("bla " + connectionString);

			return connectionString;
		}
	}
}
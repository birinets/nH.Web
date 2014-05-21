using System;
using System.Configuration;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using LightInject;

using nH.Data;
using nH.Data.Models;
using nH.Data.Repositories;
using nH.Services;
using nH.Tracing;
using nH.Web.Models;

namespace nH.Web
{
	public class DiConfig
	{
		public static void Register(ServiceContainer container, HttpConfiguration config)
		{
			container.RegisterApiControllers();

			container.Register<ICacheContext, CacheContext>(new PerContainerLifetime());
			container.Register<IDbContext>(f => new NhDbContext(GetConnectionString()), new PerContainerLifetime());
			container.Register<IPluralizeProxy, PluralizeProxy>();
			container.Register<IDiagnosticTrace, DiagnosticTrace>();
			container.Register<IDataRepository<Repository>, DataRepository<Repository>>();
			container.Register<IDataRepository<Session>, DataRepository<Session>>();
			container.Register<IDataRepository<LogEntry>, DataRepository<LogEntry>>();
			container.Register(factory => PluralizationService.CreateService(CultureInfo.GetCultureInfo("en")));

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
				InitialCatalog = uri.Host,
				DataSource = uri.AbsolutePath.Trim('/'),
				UserID = uri.UserInfo.Split(':').First(),
				Password = uri.UserInfo.Split(':').Last(),
				MultipleActiveResultSets = true
			}.ConnectionString;

			return connectionString;
		}
	}
}
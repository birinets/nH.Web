using System.Data.Entity;
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
			container.Register<DbContext, NuGetDataContext>(new PerContainerLifetime());
			container.Register<IDataRepository<Repository>, DataRepository<Repository, NuGetDataContext>>();
			container.Register<IDataRepository<Session>, DataRepository<Session, NuGetDataContext>>();
			container.Register<IDataRepository<LogEntry>, DataRepository<LogEntry, NuGetDataContext>>();

			container.EnablePerWebRequestScope();
			container.EnableWebApi(config);
		}
	}
}
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LightInject;

using nH.Web.Infrastructure;

namespace nH.Web
{
	public class MvcApplication : HttpApplication
	{
		private CacheUpdater _updater;
		private ServiceContainer _container;

		protected void Application_Start()
		{
			foreach (ConnectionStringSettings c in ConfigurationManager.ConnectionStrings)
			{
				Trace.TraceInformation("Curr conn Name: " + c.Name);
				Trace.TraceInformation("Curr conn Provider: " + c.ProviderName);
				Trace.TraceInformation("Curr conn Str: " + c.ConnectionString);
			}

			var appSettings = ConfigurationManager.AppSettings;
			for (var i = 0; i < appSettings.Count; i++)
			{
				Trace.TraceInformation("App sett #{0} Key: {1} Value: {2}", 
					i, appSettings.GetKey(i), appSettings[i]);
			}

			_container = new ServiceContainer();

			AreaRegistration.RegisterAllAreas();

			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			DiConfig.Register(_container, GlobalConfiguration.Configuration);

			_updater = new CacheUpdater(_container);
		}

		protected void Application_End()
		{
			_updater.Dispose();
			_container.Dispose();
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			var exception = Server.GetLastError();
			Trace.TraceInformation(exception.Message + " StackTrace: " + exception.StackTrace);
		}
	}
}
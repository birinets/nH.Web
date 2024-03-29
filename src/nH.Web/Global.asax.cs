﻿using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LightInject;
using StackExchange.Profiling;

using nH.Web.Infrastructure;

namespace nH.Web
{
	public class MvcApplication : HttpApplication
	{
		private CacheUpdater _updater;
		private ServiceContainer _container;

		protected void Application_Start()
		{
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

		protected void Application_BeginRequest()
		{
			if (Request.IsLocal)
			{
				MiniProfiler.Start();
			}
		}

		protected void Application_EndRequest()
		{
			MiniProfiler.Stop();
		}
	}
}
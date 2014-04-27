using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LightInject;
using nH.Data.Models;
using nH.Data.Repositories;
using nH.Web.Models;
using nH.Web.Threading;

namespace nH.Web.Infrastructure
{
	public class CacheUpdater : IDisposable
	{
		private readonly ITimer _timer;
		private readonly IServiceContainer _container;

		public CacheUpdater(IServiceContainer container)
		{
			_container = container;

			_timer = new ThreadingTimer(10);
			_timer.Tick += (sender, args) => Update();
			_timer.Start();
		}

		private void Update()
		{
			var context = _container.GetInstance<ICacheContext>();
			if (!context.Cache.ContainsKey("MainView"))
			{
				context.Cache.Add("MainView", null);
			}

			context.Cache["MainView"] = from e in GetRepository<LogEntry>().GetAll()
				join s in GetRepository<Session>().GetAll() on e.SessionId equals s.Id
				join r in GetRepository<Repository>().GetAll() on s.RepositoryId equals r.Id
				select new RootView
				{
					Id = e.Id,
					Created = e.StartDate,
					Message = e.Message,
					RepoName = r.Name
				};
		}

		public IDataRepository<T> GetRepository<T>() where T : class
		{
			return _container.GetInstance<IDataRepository<T>>();
		}

		public void Dispose()
		{
			_timer.Dispose();
		}
	}

	class DataEntry
	{
		public Guid Id { get; private set; }
		public string Message { get; private set; }

		public DataEntry(Guid id, string message)
		{
			Id = id;
			Message = message;
		}
	}
}
using System;
using System.Diagnostics;
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

			try
			{
				context.Cache[CacheKeys.RootView] = (from e in GetRepository<LogEntry>().GetAll()
					join s in GetRepository<Session>().GetAll() on e.SessionId equals s.Id
					join r in GetRepository<Repository>().GetAll() on s.RepositoryId equals r.Id
					orderby e.Created descending
					select new RootView
					{
						Id = e.Id,
						Created = e.Created,
						Message = e.Message,
						CommitId = s.CommitId,
						RepoName = r.Name,
						Type = e.Type.ToString()
					})
					.Take(50)
					.OrderBy(e => e.Created);
			}
			catch (Exception ex)
			{
				Trace.TraceError("*Exception: {0} StackTrace: {1}", ex.Message, ex.StackTrace);
			}
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
}
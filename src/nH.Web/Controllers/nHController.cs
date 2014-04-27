using System;
using System.Linq;
using System.Web.Http;

using nH.Web.Models;

namespace nH.Web.Controllers
{
	public class nHController : ApiController
	{
		private readonly ICacheContext _cacheContext;

		public nHController(ICacheContext cacheContext)
		{
			_cacheContext = cacheContext;
		}

		public IQueryable Get()
		{
			//return _cacheContext.Cache.ContainsKey(CacheKeys.RootView)
			//	? _cacheContext.Cache[CacheKeys.RootView]
			//	: null;
			return null;
		}

		public IQueryable Get(DateTime date)
		{
			if (!_cacheContext.Cache.ContainsKey(CacheKeys.RootView))
			{
				return null;
			}

			return from e in (IQueryable<RootView>) _cacheContext.Cache[CacheKeys.RootView]
				where e.Created > date
				select e;
		}
	}
}

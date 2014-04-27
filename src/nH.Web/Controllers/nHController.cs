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
			return _cacheContext.Cache.ContainsKey("MainView")
				? _cacheContext.Cache["MainView"]
				: null;
		}
	}
}

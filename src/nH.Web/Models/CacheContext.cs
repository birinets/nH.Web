using System.Collections.Generic;
using System.Linq;

namespace nH.Web.Models
{
	public class CacheContext : ICacheContext
	{
		public Dictionary<CacheKeys, IQueryable> Cache { get; set; }

		public CacheContext()
		{
			Cache = new Dictionary<CacheKeys, IQueryable>();
		}
	}
}
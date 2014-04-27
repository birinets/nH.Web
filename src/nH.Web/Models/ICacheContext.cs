using System.Collections.Generic;
using System.Linq;

namespace nH.Web.Models
{
	public interface ICacheContext
	{
		Dictionary<CacheKeys, IQueryable> Cache { get; set; }
	}
}
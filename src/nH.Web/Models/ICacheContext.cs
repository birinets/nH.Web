using System.Collections.Generic;
using System.Linq;

namespace nH.Web.Models
{
	public interface ICacheContext
	{
		Dictionary<string, IQueryable> Cache { get; set; }
	}
}
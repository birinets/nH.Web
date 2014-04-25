using System.Collections.Generic;
using System.Web.Http;

namespace nH.Web.Controllers
{
	public class nHController : ApiController
	{
		// GET api/entries
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/entries/5
		public string Get(int id)
		{
			return "value";
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;

namespace nH.Web.Controllers
{
	public class nHController : ApiController
	{
		public IEnumerable<string> Get()
		{
			return new []
			{
				DateTime.Now.ToString(CultureInfo.InvariantCulture),
				DateTime.Now.ToString(CultureInfo.InvariantCulture),
				DateTime.Now.ToString(CultureInfo.InvariantCulture),
				DateTime.Now.ToString(CultureInfo.InvariantCulture),
				DateTime.Now.ToString(CultureInfo.InvariantCulture),
				DateTime.Now.ToString(CultureInfo.InvariantCulture),
				DateTime.Now.ToString(CultureInfo.InvariantCulture)
			};
		}
	}
}

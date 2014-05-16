using System.Web.Mvc;

namespace nH.Web.Controllers
{
	public class StreamController : Controller
	{
		public ActionResult Index(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}
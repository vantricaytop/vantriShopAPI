using Microsoft.AspNetCore.Mvc;

namespace VanTriShop.Web.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

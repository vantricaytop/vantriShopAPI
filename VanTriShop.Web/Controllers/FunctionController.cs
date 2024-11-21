using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using VanTriShop.Service;
using VanTriShop.Web.Infrastructure.Core;

namespace VanTriShop.Web.Controllers
{
	[Authorize]
	[RoutePrefix("api/function")]
	public class FunctionController : ApiControllerBase
	{
		public FunctionController(IErrorService errorService) : base(errorService)
		{
		}

		public IActionResult Index()
		{
			return null;
		}
	}
}

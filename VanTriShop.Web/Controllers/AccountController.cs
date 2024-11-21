using System.Web.Http;
using VanTriShop.Service;
using VanTriShop.Web.Infrastructure.Core;

namespace VanTriShop.Web.Controllers
{
	[Authorize]
	[RoutePrefix("api/Account")]
	public class AccountController : ApiControllerBase
	{
		public AccountController(IErrorService errorService) : base(errorService)
		{

		}

		

	}
}

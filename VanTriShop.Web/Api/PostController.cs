using VanTriShop.Service;
using VanTriShop.Web.Infrastructure.Core;

namespace VanTriShop.Web.Api
{
	public class PostController : ApiControllerBase
	{
		public PostController(IErrorService errorService) : base(errorService)
		{
		}
	}
}

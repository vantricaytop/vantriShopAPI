using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IMenuRepository : IRepository<Menu>
	{
	}

	public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
	{
		public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

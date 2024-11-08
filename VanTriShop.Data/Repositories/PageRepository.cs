using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IPageRepository : IRepository<Page>
	{
	}

	public class PageRepository : RepositoryBase<Page>, IPageRepository
	{
		public PageRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

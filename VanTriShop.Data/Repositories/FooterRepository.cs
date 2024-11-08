using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IFooterRepository : IRepository<Footer>
	{
	}

	public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
	{
		public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

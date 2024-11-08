using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IProductTagRepository : IRepository<ProductTag>
	{
	}

	public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagRepository
	{
		public ProductTagRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

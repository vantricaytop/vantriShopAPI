using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IProductCategoryRepository
	{
		IEnumerable<ProductCategory> GetByAlias(String alias);
	}
	public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
	{
		public ProductCategoryRepository(DbFactory dbFactory) : base(dbFactory)
		{

		}

		public IEnumerable<ProductCategory> GetByAlias(string alias)
		{
			return this.Context.ProductCategories.Where(c => c.Alias == alias);
		}
	}
}

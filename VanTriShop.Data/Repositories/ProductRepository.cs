using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IProductRepository : IRepository<Product>
	{

	}
	public class ProductRepository : RepositoryBase<Product>, IProductRepository
	{
		public ProductRepository(IDbFactory dbFactory) : base(dbFactory) { }
	}
}

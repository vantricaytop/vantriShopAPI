using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{
	}

	public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
	{
		public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

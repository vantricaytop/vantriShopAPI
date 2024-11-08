using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IVisitorStatisticRepository : IRepository<VisitorStatistic>
	{
	}

	public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitorStatisticRepository
	{
		public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

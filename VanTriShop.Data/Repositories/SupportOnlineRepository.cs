using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface ISupportOnlineRepository : IRepository<SupportOnline>
	{
	}

	public class SupportOnlineRepository : RepositoryBase<SupportOnline>, ISupportOnlineRepository
	{
		public SupportOnlineRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

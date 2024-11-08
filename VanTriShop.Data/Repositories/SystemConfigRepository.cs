using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface ISystemConfigRepository : IRepository<SystemConfig>
	{
	}

	public class SystemConfigRepository : RepositoryBase<SystemConfig>, ISystemConfigRepository
	{
		public SystemConfigRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

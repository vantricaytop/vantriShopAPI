using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IErrorRepository : IRepository<Error>
	{

	}
	public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
	{
		public ErrorRepository(IDbFactory bFactory) : base(bFactory)
		{
		}
	}
}

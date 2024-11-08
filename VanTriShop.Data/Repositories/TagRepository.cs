using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface ITagRepository : IRepository<Tag>
	{
	}

	public class TagRepository : RepositoryBase<Tag>, ITagRepository
	{
		public TagRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

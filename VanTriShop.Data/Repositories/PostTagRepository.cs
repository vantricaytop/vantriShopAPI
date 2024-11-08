using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IPostTagRepository : IRepository<PostTag>
	{
	}

	public class PostTagRepository : RepositoryBase<PostTag>, IPostTagRepository
	{
		public PostTagRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

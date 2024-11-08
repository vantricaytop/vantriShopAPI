using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface ISlideRepository : IRepository<Slide>
	{
	}

	public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
	{
		public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Model.Models;

namespace VanTriShop.Data.Repositories
{
	public interface IPostRepository : IRepository<Post>
	{
		IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
	}

	public class PostRepository : RepositoryBase<Post>, IPostRepository
	{
		public PostRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
		{
			var query = from p in Context.Posts
						join pt in Context.PostTags
						on p.Id equals pt.PostID
						where pt.TagID == tag && p.Status
						orderby p.CreatedDate descending
						select p;

			totalRow = query.Count();

			query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

			return query;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VanTriShop.Data.Infrastructure
{
	public abstract class RepositoryBase<T> : IRepository<T> where T : class
	{
		#region Properties
		private ShopDbContext? _context;
		private readonly DbSet<T> _dbSet;

		protected IDbFactory DbFactory { get; private set; }

		protected ShopDbContext Context { get { return _context ?? (_context = DbFactory.Init()); } }
		#endregion

		protected RepositoryBase(IDbFactory dbFactory)
		{
			DbFactory = dbFactory;
			_dbSet = Context.Set<T>();
		}

		#region Implementation
		public virtual T Add(T entity)
		{
			return _dbSet.Add(entity).Entity;
		}

		public virtual void Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public virtual T Delete(int id)
		{
			var entity = _dbSet.Find(id);
			return _dbSet.Remove(entity).Entity;
		}

		public virtual void DeleteMulti(Expression<Func<T, bool>> where)
		{
			IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
			foreach (T entity in objects)
			{
				_dbSet.Remove(entity);
			}
		}

		public virtual T GetSingleById(int id)
		{
			return _dbSet.Find(id);
		}

		public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
		{
			return _dbSet.Where(where).ToList();
		}

		public virtual int Count(Expression<Func<T, bool>> where)
		{
			return _dbSet.Count(where);
		}

		public IEnumerable<T> GetAll(string[] includes = null)
		{
			//Handle includes for associated objects if applicable
			if (includes != null && includes.Count() > 0)
			{
				var query = _context.Set<T>().Include(includes.First());
				foreach(var include in includes.Skip(1))
					query = query.Include(include);
				return query.AsQueryable();
			}
			return _context.Set<T>().AsQueryable();
		}
			
		public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
		{
			if (includes != null && includes.Count() > 0) {
				var query = _context.Set<T>().Include(includes.First());
				foreach(var include in includes.Skip(1))
				{
					query = query.Include(include);
				}
				return query.FirstOrDefault(expression);
			}
			return _context.Set<T>().FirstOrDefault();
		}

		public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null )
		{
			//HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
			if (includes != null && includes.Count() > 0)
			{
				var query = _context.Set<T>().Include(includes.First());
				foreach (var include in includes.Skip(1))
					query = query.Include(include);
				return query.Where<T>(predicate).AsQueryable<T>();
			}

			return _context.Set<T>().Where<T>(predicate).AsQueryable<T>();
		}

		public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
		{
			int skipCount = index * size;
			IQueryable<T> _resetSet;

			if (includes != null && includes.Count() > 0)
			{
				var query = _context.Set<T>().Include(includes.First());
				foreach (var include in includes.Skip(1))
					query = query.Include(include);
				_resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
			} else
			{
				_resetSet = predicate != null ? _context.Set<T>().Where<T>(predicate).AsQueryable() : _context.Set<T>().AsQueryable();
			}

			_resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
			total = _resetSet.Count();
			return _resetSet.AsQueryable();

		}

		public bool CheckContains(Expression<Func<T, bool>> predicate)
		{
			return _context.Set<T>().Count<T>(predicate) > 0;
		}

		public T Delete(T entity)
		{
			return _dbSet.Remove(entity).Entity;
		}

		#endregion
	}
}
 
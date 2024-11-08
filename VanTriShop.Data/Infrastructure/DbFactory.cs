using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Data.Infrastructure
{
	public class DbFactory : Disposable, IDbFactory
	{
		private ShopDbContext context;
		public ShopDbContext Init()
		{
			return context ?? (context = new ShopDbContext());
		}

		protected override void DisposeCore()
		{
			if (context != null)
			{
				context.Dispose();
			}
		}
	}
}

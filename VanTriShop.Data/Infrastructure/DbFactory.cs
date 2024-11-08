using Microsoft.EntityFrameworkCore;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data;

public class DbFactory : Disposable, IDbFactory
{
	private ShopDbContext context;
	private DbContextOptions<ShopDbContext> _options;

	// Constructor nhận vào connectionString và tạo DbContextOptions
	public DbFactory(string connectionString)
	{
		var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
		optionsBuilder.UseSqlServer(connectionString); 
		_options = optionsBuilder.Options;
	}

	public ShopDbContext Init()
	{
		return context ?? (context = new ShopDbContext(_options));
	}

	protected override void DisposeCore()
	{
		if (context != null)
		{
			context.Dispose();
		}
	}
}

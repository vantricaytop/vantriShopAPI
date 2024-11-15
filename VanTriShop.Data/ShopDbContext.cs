using Microsoft.EntityFrameworkCore;
using VanTriShop.Model.Models;

namespace VanTriShop.Data
{
	public class ShopDbContext : DbContext
	{
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
		{
		}

		public DbSet<Footer> Footers { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<MenuGroup> MenuGroups { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Page> Pages { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<PostCategory> PostCategories { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<ProductTag> ProductTags { get; set; }
		public DbSet<Slide> Slides { get; set; }
		public DbSet<SupportOnline> SupportOnlines { get; set; }
		public DbSet<SystemConfig> SystemConfigs { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<VisitorStatistic> VisitorStatistics { get; set; }
		public DbSet<Error> Errors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderDetail>()
		   .HasKey(od => new { od.OrderId, od.ProductId });

			modelBuilder.Entity<PostTag>()
		   .HasKey(od => new { od.TagID, od.PostID });

			modelBuilder.Entity<ProductTag>()
		   .HasKey(od => new { od.TagID, od.ProductID });


			base.OnModelCreating(modelBuilder);

		}
	}
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VanTriShop.Model.Models;

namespace VanTriShop.Data
{
	public class ShopDbContext : IdentityDbContext<AppUser>
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


		public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
		public DbSet<Error> Errors { set; get; }
		public DbSet<ContactDetail> ContactDetails { set; get; }
		public DbSet<Feedback> Feedbacks { set; get; }

		public DbSet<Function> Functions { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<AppRole> AppRoles { get; set; }
		public DbSet<IdentityUserRole<string>> UserRoles {  get; set; }

		public DbSet<Color> Colors { set; get; }
		public DbSet<Size> Sizes { set; get; }
		public DbSet<ProductQuantity> ProductQuantities { set; get; }
		public DbSet<ProductImage> ProductImages { set; get; }

		public DbSet<Announcement> Announcements { set; get; }
		public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AppUser>().ToTable("AppUsers");
			modelBuilder.Entity<IdentityRole>().ToTable("AppRoles");

			modelBuilder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable("AppUserRoles"); // Đặt tên bảng
				entity.HasKey(i => new { i.UserId, i.RoleId }); // Cấu hình khóa chính
			});

			modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.ToTable("AppUserLogins");
				entity.HasKey(i => i.UserId);
			});

			modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
			{
				entity.ToTable("AppUserClaims");
				entity.HasKey(i => i.Id);
			});

			modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
			{
				entity.ToTable("RoleClaims");
			});
			modelBuilder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.ToTable("UserTokens");
			});

			modelBuilder.Entity<Function>()
				.HasOne(f => f.Parent)
				.WithMany()
				.HasForeignKey(f => f.ParentId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Permission>()
				.HasOne(f => f.Function)
				.WithMany()
				.HasForeignKey(f => f.FunctionId)
				.OnDelete(DeleteBehavior.NoAction);


			modelBuilder.Entity<OrderDetail>()
		   .HasKey(od => new { od.OrderId, od.ProductId });

			modelBuilder.Entity<PostTag>()
		   .HasKey(od => new { od.TagID, od.PostID });

			modelBuilder.Entity<ProductTag>()
		   .HasKey(od => new { od.TagID, od.ProductID });

			modelBuilder.Entity<AnnouncementUser>().HasKey(od => new { od.AnnouncementId, od.UserId });

			modelBuilder.Entity<ProductQuantity>().HasKey(od => new { od.ProductId, od.SizeId, od.ColorId });



		}
	}
}

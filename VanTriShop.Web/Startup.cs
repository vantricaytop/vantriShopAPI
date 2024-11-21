using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data.Repositories;
using VanTriShop.Data;
using VanTriShop.Model.Models;
using VanTriShop.Service;
using VanTriShop.Web.Mappings;
using Autofac.Core;
using Microsoft.OpenApi.Models;

public class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "VanTriShop API",
				Version = "v1",
				Description = "API cho hệ thống cửa hàng VanTriShop"
			});
		});


		// Thêm AutoMapper
		services.AddAutoMapper(typeof(AutoMapperConfiguration));

		// Cấu hình CORS
		ConfigureCors(services);

		// Cấu hình DbContext
		ConfigureDatabase(services);

		// Cấu hình ASP.NET Identity
		ConfigureIdentity(services);

		// Cấu hình JWT Authentication
		ConfigureJwtAuthentication(services);

		// Thêm MVC
		services.AddControllers();
	}

	private void ConfigureCors(IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", builder =>
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader());
		});
	}

	private void ConfigureDatabase(IServiceCollection services)
	{
		services.AddDbContext<ShopDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("VantriShopDb")));
	}

	private void ConfigureIdentity(IServiceCollection services)
	{
		services.AddIdentity<AppUser, IdentityRole>(options =>
		{
			options.Password.RequiredLength = 6;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireDigit = true;

			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.AllowedForNewUsers = true;

			options.User.RequireUniqueEmail = true;
		})
		.AddEntityFrameworkStores<ShopDbContext>()
		.AddDefaultTokenProviders();
	}

	private void ConfigureJwtAuthentication(IServiceCollection services)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = Configuration["Jwt:Issuer"],
				ValidAudience = Configuration["Jwt:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
			};
		});
	}

	public void ConfigureContainer(ContainerBuilder builder)
	{
		builder.RegisterInstance(Configuration).As<IConfiguration>().SingleInstance();

		// Đăng ký DbFactory sử dụng delegate
		builder.Register(c =>
		{
			var connectionString = Configuration.GetConnectionString("VantriShopDb");
			return new DbFactory(connectionString);
		})
		.As<IDbFactory>()
		.InstancePerLifetimeScope();

		builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
		builder.RegisterType<ShopDbContext>().AsSelf().InstancePerLifetimeScope();
		builder.RegisterType<ErrorService>().As<IErrorService>().InstancePerLifetimeScope();

		builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
			   .Where(t => t.Name.EndsWith("Repository"))
			   .AsImplementedInterfaces()
			   .InstancePerLifetimeScope();

		builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
			   .Where(t => t.Name.EndsWith("Service"))
			   .AsImplementedInterfaces()
			   .InstancePerLifetimeScope();
	}


	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseCors("AllowAll");

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints => endpoints.MapControllers());
	}
}

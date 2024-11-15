using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VanTriShop.Data;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data.Repositories;
using VanTriShop.Service;
using VanTriShop.Web.Mappings;

namespace VanTriShop.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			services.AddAutoMapper(typeof(AutoMapperConfiguration));
		}

		// Phương thức ConfigAutofac được điều chỉnh thành ConfigureContainer
		public void ConfigureContainer(ContainerBuilder builder)
		{
			// Đăng ký IConfiguration cho Autofac
			builder.RegisterInstance(Configuration)
				   .As<IConfiguration>()
				   .SingleInstance();

			// Lấy connectionString từ appsettings.json
			var connectionString = Configuration.GetConnectionString("VantriShopDb");

			// Đăng ký DbFactory và truyền connectionString
			builder.RegisterType<DbFactory>()
				   .As<IDbFactory>()
				   .WithParameter("connectionString", connectionString)
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

		public void Configure(IApplicationBuilder app, IHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

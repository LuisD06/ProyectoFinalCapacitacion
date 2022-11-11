using System.Diagnostics;
using Curso.ECommerce.Domain.Repository;
using Curso.ECommerce.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ECommerce.Infraestructure
{
    public static class InfraestructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddDbContext<ECommerceDbContext>(options =>
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                var dbPath = Path.Join(path, configuration.GetConnectionString("ECommerce"));
                Debug.WriteLine($"dbPath: {dbPath}");

                options.UseSqlite($"Data Source={dbPath}");
            });

            return services;
        }

    }
}
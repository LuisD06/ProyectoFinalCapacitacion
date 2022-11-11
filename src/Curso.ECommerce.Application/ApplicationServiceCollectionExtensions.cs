using System.Reflection;
using Curso.ECommerce.Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ECommerce.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBrandAppService, BrandAppService>();
            services.AddTransient<IProductTypeAppService, ProductTypeAppService>();
            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<IClientAppService, ClientAppService>();
            services.AddTransient<IOrderAppService, OrderAppSerivce>();

            //Configurar la inyección de todos los profile que existen en un Assembly
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
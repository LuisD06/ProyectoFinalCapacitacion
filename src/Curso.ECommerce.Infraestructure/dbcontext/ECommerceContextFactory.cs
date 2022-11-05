using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Curso.ECommerce.Infraestructure
{
    public class ECommerceContextFactory : IDesignTimeDbContextFactory<ECommerceDbContext>
    {
        public ECommerceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Curso.ECommerce.HttpApi"))
                .AddJsonFile("appsettings.json")
                .Build();
            
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, configuration.GetConnectionString("ECommerce"));
            Debug.WriteLine($"dbPath: {dbPath}");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            return new ECommerceDbContext(optionsBuilder.Options);
        }
    }
}
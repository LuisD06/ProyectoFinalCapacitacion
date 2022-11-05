using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ECommerce.Infraestructure
{
    public class ECommerceDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Brand> Brands { get; set; }
        // public DbSet<Client> Clients { get; set; }
        // public DbSet<Order> Order { get; set; }
        // public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        
        
        public string DbPath { get; set; }
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        // TODO: Add convertions for sqlite limitations
        // Reference: https://learn.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
    }
}
using Curso.ComercioElectronico.Domain.models;
using Curso.ComercioElectronico.Domain.repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class ECommerceDbContext : DbContext, IUnitOfWork
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        
        public string DbPath { get; set; }

        // TODO: Add convertions for sqlite limitations
        // Reference: https://learn.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
    }
}
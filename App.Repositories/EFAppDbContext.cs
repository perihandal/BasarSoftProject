using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories
{
    public class EFAppDbContext : DbContext
    {

        public EFAppDbContext(DbContextOptions<EFAppDbContext> options) : base(options)
        { 
        }
        public DbSet<GeometryEntity> Geometries { get; set; } = default!;
        public DbSet<GeometryInfoEntity> GeometryInfos { get; set; } = default!;
        public DbSet<GeometryMetricsEntity> GeometryMetrics { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

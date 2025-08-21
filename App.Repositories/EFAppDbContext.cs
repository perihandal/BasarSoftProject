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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Assembly içindeki tüm IEntityTypeConfiguration implementasyonlarını uygula
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

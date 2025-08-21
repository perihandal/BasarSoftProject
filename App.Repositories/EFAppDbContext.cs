using App.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace App.Repositories
{
    public class EFAppDbContext : DbContext
    {
        public DbSet<GeometryEntity> Geometries { get; set; }

        public EFAppDbContext(DbContextOptions<EFAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeometryEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(g => g.Geoloc)
                 .HasColumnType("geoloc"); // PostGIS için
            });
        }

    }
}

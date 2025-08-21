using BasarSoftProject.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasarSoftProject.Repositories
{
    public class EFAppDbContext:DbContext
    {
            public DbSet<GeometryEntity> Geometries { get; set; }

            public EFAppDbContext(DbContextOptions<EFAppDbContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<GeometryEntity>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Name).IsRequired();
                    entity.Property(e => e.Wkt).IsRequired();
                    entity.Property(e => e.Geometry).HasColumnType("geometry");
                });
            }
      
    }
}

using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Geometries
{
    public class GeometryMetricsEntityConfiguration : IEntityTypeConfiguration<GeometryMetricsEntity>
    {
        public void Configure(EntityTypeBuilder<GeometryMetricsEntity> builder)
        {
            builder.ToTable("GeometryMetrics");

            builder.HasKey(g => g.GeometryId); // PK = FK

            // Polygon alanları
            builder.Property(g => g.Area);
            builder.Property(g => g.Centroid)
                   .HasColumnType("geometry(Point,4326)")
                   .IsRequired(false);
            builder.Property(g => g.BoundingBox)
                   .HasColumnType("geometry(Polygon,4326)")
                   .IsRequired(false);

            // LineString alanları
            builder.Property(g => g.Length);
            builder.Property(g => g.StartPoint)
                   .HasColumnType("geometry(Point,4326)")
                   .IsRequired(false);


            builder.Property(g => g.EndPoint)
                   .HasColumnType("geometry(Point,4326)")
                   .IsRequired(false);


            // One-to-One ilişki GeometryEntity ile
            builder.HasOne(g => g.GeometryEntity)
                   .WithOne(g => g.GeometryMetrics)
                   .HasForeignKey<GeometryMetricsEntity>(g => g.GeometryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

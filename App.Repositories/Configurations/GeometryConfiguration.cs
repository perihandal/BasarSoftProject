using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite;
using NetTopologySuite.Geometries;

namespace App.Repositories.Geometries
{
    public class GeometryEntityConfiguration : IEntityTypeConfiguration<GeometryEntity>
    {
        public void Configure(EntityTypeBuilder<GeometryEntity> builder)
        {
            builder.ToTable("Geometries");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(g => g.Wkt)
                   .IsRequired();

            builder.Property(g => g.Type)
                   .IsRequired();
            //builder.Property(g => g.Geoloc).HasColumnType("geometry(Geometry,4326").IsRequired();

            // Navigasyon propertyleri ile ilişki GeometryInfo ve GeometryMetrics
            builder.HasOne(g => g.GeometryInfo)
                   .WithOne(g => g.GeometryEntity)
                   .HasForeignKey<GeometryInfoEntity>(g => g.GeometryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.GeometryMetrics)
                   .WithOne(g => g.GeometryEntity)
                   .HasForeignKey<GeometryMetricsEntity>(g => g.GeometryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

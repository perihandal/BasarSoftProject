using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure; 

namespace App.Repositories.Geometries
{
    public class GeometryConfiguration : IEntityTypeConfiguration<GeometryEntity>
    {
        public void Configure(EntityTypeBuilder<GeometryEntity> builder)
        {
            builder.ToTable("Geometries");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                   .ValueGeneratedOnAdd(); // Auto increment

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(g => g.Wkt)
                   .IsRequired();

            builder.Property(g => g.Type)
                   .IsRequired();

            //builder.Property(g => g.Geoloc)
            //       .HasColumnType("geometry")
            //       .HasSrid(4326); // WGS84         // SRID (WGS84)
        }
    }
}

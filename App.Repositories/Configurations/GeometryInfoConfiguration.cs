using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Geometries
{
    public class GeometryInfoConfiguration : IEntityTypeConfiguration<GeometryInfoEntity>
    {
        public void Configure(EntityTypeBuilder<GeometryInfoEntity> builder)
        {
            builder.ToTable("GeometryInfos");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(g => g.FullAddress)
                   .HasMaxLength(500);

            builder.Property(g => g.Phone)
                   .HasMaxLength(50);

            builder.Property(g => g.PhotoBase64);

            builder.Property(g => g.Description);

            builder.Property(g => g.OpeningHours);
            builder.Property(g => g.CreatedAt)
                   .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            builder.Property(g => g.UpdatedAt)
                   .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            // One-to-One ilişki GeometryEntity ile
            builder.HasOne(g => g.GeometryEntity)
                   .WithOne(g => g.GeometryInfo)
                   .HasForeignKey<GeometryInfoEntity>(g => g.GeometryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

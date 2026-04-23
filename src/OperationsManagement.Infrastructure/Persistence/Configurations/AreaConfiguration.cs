using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Infrastructure.Persistence.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Areas");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

        // Enforce the Foreign Key to the Site Aggregate
        builder.HasOne<Site>()
               .WithMany() // Site doesn't have a Navigation Property of Areas
               .HasForeignKey(a => a.SiteId)
               .OnDelete(DeleteBehavior.Restrict);

        // Mapping the private collection of ProcessCellIds
        builder.Property(a => a.ProcessCellIds)
               .HasField("_processCellIds")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
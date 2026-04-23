using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Infrastructure.Persistence.Configurations;

public class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.ToTable("Sites");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Description).HasMaxLength(500);

        // DDD Reference: We don't map a physical relationship to Area here 
        // because Area is its own Aggregate Root. 
        // We ignore the _areaIds collection or map it as a primitive.
        builder.Property(s => s.AreaIds)
               .HasField("_areaIds") // Use the private backing field
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceExecution.Domain.ResourceManagement.Aggregates;

namespace ResourceExecution.Infrastructure.Persistence.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Areas");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired(false);

        // Enforce the Foreign Key to the Site Aggregate
        builder.HasOne<Site>()
               .WithMany() // Site doesn't have a Navigation Property of Areas
               .HasForeignKey(a => a.SiteId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);

        builder.HasMany<WorkCenter>()
           .WithOne()
           .HasForeignKey(x => x.AreaId)
           .OnDelete(DeleteBehavior.Cascade);

        var navigation = builder.Metadata.FindNavigation(nameof(Area.ProcessCellIds));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
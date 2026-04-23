using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Entities;

namespace ResourceExecution.Infrastructure.Persistence.Configurations;

public class ProcessCellConfiguration : IEntityTypeConfiguration<WorkCenter>
{
    public void Configure(EntityTypeBuilder<WorkCenter> builder)
    {
        builder.ToTable("ProcessCells");
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pc => pc.Description)
            .IsRequired(false);

        // Enforce FK to Area Aggregate
        builder.HasOne<Area>()
               .WithMany()
               .HasForeignKey(pc => pc.AreaId)
               .OnDelete(DeleteBehavior.Restrict);

        // This is where you connect to the Units you mentioned earlier
        builder.OwnsMany(pc => pc.Units, unitBuilder =>
        {
            unitBuilder.ToTable("Units");
            unitBuilder.HasKey(u => u.Id);

            // Link to Parent Aggregate
            unitBuilder.WithOwner()
            .HasForeignKey(nameof(WorkUnit.ProcessCellId));

            unitBuilder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            unitBuilder.Property(u => u.Description)
                .IsRequired(false);

            unitBuilder.Property(u => u.ProcessSegmentId)
                .IsRequired(true);

            unitBuilder.Property(u => u.Status)
                .HasConversion<string>()
                .IsRequired(true);

            // Configuring the Complex Property: UnsAddress
            // This flattens the UnsAddress properties into the Units table
            unitBuilder.OwnsOne(u => u.UnsAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.Enterprise)
                    .HasColumnName("Uns_Enterprise")
                    .HasMaxLength(100);

                addressBuilder.Property(a => a.Site)
                    .HasColumnName("Uns_Site")
                    .HasMaxLength(100);

                addressBuilder.Property(a => a.Area)
                    .HasColumnName("Uns_Area")
                    .HasMaxLength(100);

                addressBuilder.Property(a => a.ProcessCell)
                    .HasColumnName("Uns_ProcessCell")
                    .HasMaxLength(100);

                addressBuilder.Property(a => a.Unit)
                    .HasColumnName("Uns_Unit")
                    .HasMaxLength(100);
            });
        });
    }
}
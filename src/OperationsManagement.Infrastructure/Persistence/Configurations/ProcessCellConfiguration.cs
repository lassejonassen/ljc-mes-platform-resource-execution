using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Infrastructure.Persistence.Configurations;

public class ProcessCellConfiguration : IEntityTypeConfiguration<ProcessCell>
{
    public void Configure(EntityTypeBuilder<ProcessCell> builder)
    {
        builder.ToTable("ProcessCells", "Infrastructure");
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Name).IsRequired().HasMaxLength(100);

        builder.ToTable("ProcessCells");
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Name).IsRequired().HasMaxLength(100);

        // Enforce FK to Area Aggregate
        builder.HasOne<Area>()
               .WithMany()
               .HasForeignKey(pc => pc.AreaId)
               .OnDelete(DeleteBehavior.Restrict);

        // This is where you connect to the Units you mentioned earlier
        builder.OwnsMany(pc => pc.Units, unitBuilder =>
        {
            unitBuilder.ToTable("Units", "Infrastructure");
            unitBuilder.HasKey(u => u.Id);

            // Link to Parent Aggregate
            unitBuilder.WithOwner().HasForeignKey("ProcessCellId");

            unitBuilder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

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

                // Note: FullPath is likely a computed property in your DTO/Domain 
                // and doesn't necessarily need to be stored in the DB.
            });
        });
    }
}
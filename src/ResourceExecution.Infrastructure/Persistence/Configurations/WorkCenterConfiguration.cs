using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Entities;
using ResourceExecution.Domain.ResourceManagement.Enums;
using ResourceExecution.Domain.ResourceManagement.ValueObjects;

namespace ResourceExecution.Infrastructure.Persistence.Configurations;

public class WorkCenterConfiguration : IEntityTypeConfiguration<WorkCenter>
{
    public void Configure(EntityTypeBuilder<WorkCenter> builder)
    {
        builder.ToTable("WorkCenters");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(WorkCenter.NameMaxLength);

        builder.Property(pc => pc.Description)
            .IsRequired(false)
            .HasMaxLength(WorkCenter.DescriptionMaxLength);

        builder.OwnsMany(x => x.WorkUnits, wu =>
        {
            wu.ToTable("WorkUnits");
            wu.HasKey(wu => wu.Id);

            wu.WithOwner()
            .HasForeignKey(wu => wu.WorkCenterId);

            wu.Property(p => p.Name)
                .HasMaxLength(WorkUnit.NameMaxLength)
                .IsRequired(true);

            wu.Property(p => p.Description)
                .HasMaxLength(WorkUnit.DescriptionMaxLength)
                .IsRequired(false);

            wu.Property(p => p.EquipmentClassId)
                .IsRequired(true);

            wu.Property(p => p.Status)
                .HasConversion<string>()
                .HasDefaultValue(WorkUnitStatus.Idle)
                .IsRequired(true);

            wu.OwnsMany(x => x.Capabilities, c =>
            {
                // Maps to a separate table by default
                c.ToTable("WorkUnitCapabilities");

                // Define the foreign key back to EquipmentClass
                c.WithOwner()
                    .HasForeignKey("WorkUnitId");

                // Since Value Objects don't have an ID, EF Core needs a Shadow Key 
                // to uniquely identify the row in the database table
                c.Property<int>("Id");
                c.HasKey("Id");

                c.Property(x => x.Name)
                    .HasMaxLength(EquipmentCapability.NameMaxLength)
                    .IsRequired();

                c.Property(x => x.Value)
                    .HasMaxLength(EquipmentCapability.ValueMaxLength)
                    .IsRequired();

                c.Property(x => x.UnitOfMeasure)
                    .HasMaxLength(EquipmentCapability.UnitOfMeasureMaxLength)
                    .IsRequired();
            });
        });
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.ValueObjects;

namespace ResourceExecution.Infrastructure.Persistence.Configurations;

public class EquipmentClassConfiguration : IEntityTypeConfiguration<EquipmentClass>
{
    public void Configure(EntityTypeBuilder<EquipmentClass> builder)
    {
        builder.ToTable("EquipmentClasses");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(EquipmentClass.NameMaxLength);

        builder.Property(s => s.Description)
            .HasMaxLength(EquipmentClass.DescriptionMaxLength)
            .IsRequired(false);

        builder.OwnsMany(x => x.StandardCapabilities, cb =>
        {
            // Maps to a separate table by default
            cb.ToTable("EquipmentClassCapabilities");

            // Define the foreign key back to EquipmentClass
            cb.WithOwner()
            .HasForeignKey("EquipmentClassId");

            // Since Value Objects don't have an ID, EF Core needs a Shadow Key 
            // to uniquely identify the row in the database table
            cb.Property<int>("Id");
            cb.HasKey("Id");

            cb.Property(x => x.Name)
                .HasMaxLength(EquipmentCapability.NameMaxLength)
                .IsRequired();

            cb.Property(x => x.Value)
                .HasMaxLength(EquipmentCapability.ValueMaxLength)
                .IsRequired();

            cb.Property(x => x.UnitOfMeasure)
                .HasMaxLength(EquipmentCapability.UnitOfMeasureMaxLength)
                .IsRequired();
        });

        // Tell EF Core to use the private backing field for the navigation
        builder.Metadata.FindNavigation(nameof(EquipmentClass.StandardCapabilities))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
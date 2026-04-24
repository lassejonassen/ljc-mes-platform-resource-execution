namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses.Capabilties;

public sealed record EquipmentClassCapabilityCreateRequestDTO
{
    public required Guid EquipmentClassId { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string UnitOfMeasure { get; init; }
}
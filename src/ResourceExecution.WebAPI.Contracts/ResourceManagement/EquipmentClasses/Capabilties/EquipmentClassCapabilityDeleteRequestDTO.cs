namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses.Capabilties;

public sealed record EquipmentClassCapabilityDeleteRequestDTO
{
    public required Guid EquipmentClassId { get; init; }
    public required string Name { get; init; }
}
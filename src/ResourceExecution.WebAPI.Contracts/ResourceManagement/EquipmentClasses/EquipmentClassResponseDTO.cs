namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses;

public sealed record EquipmentClassResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<EquipmentCapabilityResponseDTO>? Capabilities { get; init; }
}

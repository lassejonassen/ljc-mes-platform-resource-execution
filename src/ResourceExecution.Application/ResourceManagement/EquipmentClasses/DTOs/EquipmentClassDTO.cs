using ResourceExecution.Application.Common.DTOs;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.DTOs;

public sealed record EquipmentClassDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<EquipmentCapabilityDTO>? Capabilities { get; init; }
}

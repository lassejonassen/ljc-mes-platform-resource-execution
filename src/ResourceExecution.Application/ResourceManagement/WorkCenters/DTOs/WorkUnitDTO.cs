using ResourceExecution.Application.Common.DTOs;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.DTOs;

public sealed class WorkUnitDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid WorkCenterId { get; init; }
    public required Guid EquipmentClassId { get; init; }
    public required string Status { get; init; }
    public IReadOnlyCollection<EquipmentCapabilityDTO>? Capabilities { get; init; }
}

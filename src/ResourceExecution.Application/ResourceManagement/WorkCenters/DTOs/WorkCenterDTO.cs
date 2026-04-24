namespace ResourceExecution.Application.ResourceManagement.WorkCenters.DTOs;

public sealed record WorkCenterDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<WorkUnitDTO>? WorkUnits { get; init; } = [];
}

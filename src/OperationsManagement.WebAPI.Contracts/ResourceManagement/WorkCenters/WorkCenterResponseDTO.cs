using ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells.Units;

namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;

public sealed record WorkCenterResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<UnitResponseDTO>? WorkUnits { get; init; } = [];
}

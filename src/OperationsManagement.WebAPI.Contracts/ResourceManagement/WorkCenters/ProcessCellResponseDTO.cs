using ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells.Units;

namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;

public sealed record ProcessCellResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid AreaId { get; init; }
    public IReadOnlyCollection<UnitResponseDTO>? Units { get; init; } = [];
}

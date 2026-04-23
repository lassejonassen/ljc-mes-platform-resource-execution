using OperationsManagement.WebAPI.Contracts.Assets.ProcessCells.Units;

namespace OperationsManagement.WebAPI.Contracts.Assets.ProcessCells;

public sealed record ProcessCellResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid AreaId { get; init; }
    public IReadOnlyCollection<UnitResponseDTO>? Units { get; init; } = [];
}

namespace OperationsManagement.Application.Assets.ProcessCells.DTOs;

public sealed record ProcessCellDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid AreaId { get; init; }
    public IReadOnlyCollection<UnitDTO>? Units { get; init; } = [];
}

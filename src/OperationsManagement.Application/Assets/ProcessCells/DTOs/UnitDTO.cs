namespace OperationsManagement.Application.Assets.ProcessCells.DTOs;

public sealed class UnitDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required string UnsEnterprise { get; init; }
    public required string UnsSite { get; init; }
    public required string UnsArea { get; init; }
    public required string UnsProcessCell { get; init; }
    public required string UnsUnit { get; init; }
    public required Guid ProcessCellId { get; init; }
    public required Guid ProcessSegmentId { get; init; }
}

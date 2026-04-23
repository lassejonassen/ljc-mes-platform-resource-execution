namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells.Units;

public sealed class UnitResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? UnsEnterprise { get; init; }
    public string? UnsSite { get; init; }
    public string? UnsArea { get; init; }
    public string? UnsProcessCell { get; init; }
    public string? UnsUnit { get; init; }
    public required Guid ProcessCellId { get; init; }
    public Guid? ProcessSegmentId { get; init; }
}

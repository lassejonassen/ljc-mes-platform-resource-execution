namespace OperationsManagement.WebAPI.Contracts.Assets.ProcessCells;

public sealed record ProcessCellUpdateRequestDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}

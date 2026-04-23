namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;

public sealed record ProcessCellCreateRequestDTO
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid SiteId { get; init; }
}
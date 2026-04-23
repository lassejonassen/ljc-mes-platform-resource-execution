namespace OperationsManagement.Application.Assets.Areas.DTOs;

public sealed record AreaDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid SiteId { get; init; }
}

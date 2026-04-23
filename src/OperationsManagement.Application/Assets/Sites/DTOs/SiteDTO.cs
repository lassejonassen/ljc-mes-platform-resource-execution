namespace OperationsManagement.Application.Assets.Sites.DTOs;

public sealed record SiteDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}

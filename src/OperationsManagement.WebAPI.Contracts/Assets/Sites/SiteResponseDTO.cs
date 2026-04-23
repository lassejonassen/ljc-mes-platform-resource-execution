namespace OperationsManagement.WebAPI.Contracts.Assets.Sites;

public sealed record SiteResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}

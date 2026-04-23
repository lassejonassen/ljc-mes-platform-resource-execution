namespace OperationsManagement.WebAPI.Contracts.Assets.Sites;

public sealed record SiteCreateRequestDTO
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}
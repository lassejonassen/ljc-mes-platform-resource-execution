namespace OperationsManagement.WebAPI.Contracts.Assets.Sites;

public sealed record SiteListResponseDTO
{
    public required IEnumerable<SiteResponseDTO> Data { get; init; }
}

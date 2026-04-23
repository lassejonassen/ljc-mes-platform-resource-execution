namespace OperationsManagement.WebAPI.Contracts.Assets.Areas;

public sealed record AreaListResponseDTO
{
    public required IEnumerable<AreaResponseDTO> Data { get; init; }
}

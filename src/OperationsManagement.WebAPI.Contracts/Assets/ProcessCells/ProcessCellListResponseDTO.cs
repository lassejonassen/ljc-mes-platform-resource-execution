namespace OperationsManagement.WebAPI.Contracts.Assets.ProcessCells;

public sealed record ProcessCellListResponseDTO
{
    public required IEnumerable<ProcessCellResponseDTO> Data { get; init; }
}

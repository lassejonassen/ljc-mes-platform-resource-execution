namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;

public sealed record ProcessCellListResponseDTO
{
    public required IEnumerable<ProcessCellResponseDTO> Data { get; init; }
}

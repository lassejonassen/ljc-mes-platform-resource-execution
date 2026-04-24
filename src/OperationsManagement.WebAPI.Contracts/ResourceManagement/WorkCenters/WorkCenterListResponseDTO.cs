namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;

public sealed record WorkCenterListResponseDTO
{
    public required IEnumerable<WorkCenterResponseDTO> Data { get; init; }
}

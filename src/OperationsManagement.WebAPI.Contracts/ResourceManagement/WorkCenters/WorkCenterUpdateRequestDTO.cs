namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;

public sealed record WorkCenterUpdateRequestDTO(Guid Id, string Name, string? Description);
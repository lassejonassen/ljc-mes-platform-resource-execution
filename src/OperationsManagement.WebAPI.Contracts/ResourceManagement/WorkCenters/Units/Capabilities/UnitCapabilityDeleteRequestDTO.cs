namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units.Capabilities;

public sealed record UnitCapabilityDeleteRequestDTO(Guid WorkCenterId, Guid WorkUnitId, string Name);

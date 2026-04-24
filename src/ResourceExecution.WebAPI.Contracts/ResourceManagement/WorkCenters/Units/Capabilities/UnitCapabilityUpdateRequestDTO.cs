namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units.Capabilities;

public sealed record UnitCapabilityUpdateRequestDTO(Guid WorkCenterId, Guid WorkUnitId, string Name, string Value, string UnitOfMeasure);

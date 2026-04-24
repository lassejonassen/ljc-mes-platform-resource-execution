namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units.Capabilities;

public sealed record UnitCapabilityCreateRequestDTO(Guid WorkCenterId, Guid WorkUnitId, string Name, string Value, string UnitOfMeasure);

namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units;

public sealed record UnitUpdateRequestDTO(Guid WorkCenterId, Guid WorkUnitId, string Name, string? Description, Guid EquipmentClassId);

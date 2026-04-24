namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units;

public sealed record UnitCreateRequestDTO(Guid WorkCenterId, string Name, string? Description, Guid EquipmentClassId);

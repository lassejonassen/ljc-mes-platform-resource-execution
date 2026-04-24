namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells.Units;

public sealed class UnitResponseDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid WorkCenterId { get; init; }
    public required Guid EquipmentClassId { get; init; }
}

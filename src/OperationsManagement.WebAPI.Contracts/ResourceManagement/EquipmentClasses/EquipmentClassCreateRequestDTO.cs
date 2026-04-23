namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses;

public sealed record EquipmentClassCreateRequestDTO
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}
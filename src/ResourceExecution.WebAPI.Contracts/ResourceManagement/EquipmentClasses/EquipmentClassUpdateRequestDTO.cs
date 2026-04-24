namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses;

public sealed record EquipmentClassUpdateRequestDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}

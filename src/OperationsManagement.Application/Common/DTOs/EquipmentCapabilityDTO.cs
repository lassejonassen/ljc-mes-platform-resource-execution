namespace ResourceExecution.Application.Common.DTOs;

public sealed record EquipmentCapabilityDTO
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string UnitOfMeasure { get; init; }
}

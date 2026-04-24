namespace ResourceExecution.WebAPI.Contracts;

public sealed record EquipmentCapabilityResponseDTO
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string UnitOfMeasure { get; init; }
}

namespace ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses;

public sealed record EquipmentClassListResponseDTO
{
    public required IReadOnlyCollection<EquipmentClassResponseDTO> Data { get; init; }
}

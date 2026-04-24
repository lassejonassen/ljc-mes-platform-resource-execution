using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Enums;
using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.ValueObjects;

namespace ResourceExecution.Domain.ResourceManagement.Entities;

public sealed class WorkUnit : AggregateRoot
{
    public const int NameMaxLength = 20;
    public const int DescriptionMaxLength = 100;

    private WorkUnit() { }
    private WorkUnit(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid WorkCenterId { get; private set; }
    public Guid EquipmentClassId { get; private set; }
    public WorkUnitStatus Status { get; private set; }

    public WorkCenter WorkCenter { get; } = null!;

    private readonly List<EquipmentCapability> _capabilities = [];
    public IReadOnlyCollection<EquipmentCapability> Capabilities => _capabilities;


    public static Result<WorkUnit> Create(
        string name,
        string? description,
        Guid workCenterId,
        Guid equipmentClassId,
        DateTime utcNow)
    {
        var unit = new WorkUnit(utcNow)
        {
            Name = name,
            Description = description,
            WorkCenterId = workCenterId,
            EquipmentClassId = equipmentClassId,
        };

        return Result.Success(unit);
    }

    public Result Update(string name, string? description, Guid equipmentClassId)
    {
        Name = name;
        Description = description;
        EquipmentClassId = equipmentClassId;

        return Result.Success();
    }

    public Result AddCapability(string name, string value, string unitOfMeasure)
    {
        bool exists = _capabilities.Any(x => x.Name == name);
        if (exists)
            return Result.Failure(EquipmentClassErrors.CapabilityExists);

        var equipmentCapability = EquipmentCapability.Create(name, value, unitOfMeasure);
        if (equipmentCapability.IsFailure)
            return Result.Failure(equipmentCapability.Error);

        _capabilities.Add(equipmentCapability.Value);

        return Result.Success();
    }

    public Result UpdateCapability(string name, string value, string unitOfMeasure)
    {
        var existingCapability = _capabilities.FirstOrDefault(x => x.Name == name);
        if (existingCapability is null)
            return Result.Failure(EquipmentClassErrors.CapabilityNotFound);

        _capabilities.Remove(existingCapability);

        var equipmentCapability = EquipmentCapability.Create(name, value, unitOfMeasure);
        if (equipmentCapability.IsFailure)
            return Result.Failure(equipmentCapability.Error);

        _capabilities.Add(equipmentCapability.Value);

        return Result.Success();
    }

    public Result RemoveCapability(string name)
    {
        var existingCapability = _capabilities.FirstOrDefault(x => x.Name == name);
        if (existingCapability is null)
            return Result.Failure(EquipmentClassErrors.CapabilityNotFound);

        _capabilities.Remove(existingCapability);

        return Result.Success();
    }
}

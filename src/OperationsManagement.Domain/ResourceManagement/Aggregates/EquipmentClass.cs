using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.ValueObjects;

namespace ResourceExecution.Domain.ResourceManagement.Aggregates;

public sealed class EquipmentClass : AggregateRoot
{
    public const int NameMaxLength = 20;
    public const int DescriptionMaxLength = 100;

    private EquipmentClass() { }
    private EquipmentClass(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private readonly List<EquipmentCapability> _standardCapabilities = [];
    public IReadOnlyCollection<EquipmentCapability> StandardCapabilities => _standardCapabilities.AsReadOnly();

    public static Result<EquipmentClass> Create(string name, string? description, DateTime utcNow)
    {
        var validationResult = ValidateInvariants(name, description);
        if (validationResult.IsFailure)
            return Result.Failure<EquipmentClass>(validationResult.Error);

        var equipmentClass = new EquipmentClass(utcNow)
        {
            Name = name,
            Description = description
        };

        return Result.Success(equipmentClass);
    }

    public Result Update(string name, string? description)
    {
        var validationResult = ValidateInvariants(name, description);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        Name = name;
        Description = description;

        return Result.Success();
    }

    public Result AddStandardCapability(string name, string value, string unitOfMeasure)
    {
        bool exists = _standardCapabilities.Any(x => x.Name == name);
        if (exists)
            return Result.Failure(EquipmentClassErrors.CapabilityExists);

        var equipmentCapability = EquipmentCapability.Create(name, value, unitOfMeasure);
        if (equipmentCapability.IsFailure)
            return Result.Failure(equipmentCapability.Error);

        _standardCapabilities.Add(equipmentCapability.Value);

        return Result.Success();
    }

    public Result UpdateStandardCapability(string name, string value, string unitOfMeasure)
    {
        var existingCapability = _standardCapabilities.FirstOrDefault(x => x.Name == name);
        if (existingCapability is null)
            return Result.Failure(EquipmentClassErrors.CapabilityNotFound);

        _standardCapabilities.Remove(existingCapability);

        var equipmentCapability = EquipmentCapability.Create(name, value, unitOfMeasure);
        if (equipmentCapability.IsFailure)
            return Result.Failure(equipmentCapability.Error);

        _standardCapabilities.Add(equipmentCapability.Value);

        return Result.Success();
    }

    public Result RemoveStandardCapability(string name)
    {
        var existingCapability = _standardCapabilities.FirstOrDefault(x => x.Name == name);
        if (existingCapability is null)
            return Result.Failure(EquipmentClassErrors.CapabilityNotFound);

        _standardCapabilities.Remove(existingCapability);

        return Result.Success();
    }

    private static Result ValidateInvariants(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(EquipmentClassErrors.NameRequired);

        if (name.Length > NameMaxLength)
            return Result.Failure(EquipmentClassErrors.NameTooLong);

        if (description is not null && description.Length > DescriptionMaxLength)
            return Result.Failure(EquipmentClassErrors.DescriptionTooLong);

        return Result.Success();
    }
}
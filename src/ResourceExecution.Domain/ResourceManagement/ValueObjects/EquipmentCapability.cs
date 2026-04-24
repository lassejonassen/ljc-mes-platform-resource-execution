namespace ResourceExecution.Domain.ResourceManagement.ValueObjects;

public sealed class EquipmentCapability(string name, string value, string unitOfMeasure) : ValueObject
{
    public const int NameMaxLength = 50;
    public const int ValueMaxLength = 100;
    public const int UnitOfMeasureMaxLength = 100;

    // Examples: "MaxPressure", "MinTemperature", "ContainerType", "MaxSpeed"
    public string Name { get; } = name;

    // The value of the capability (could be numeric or a descriptor)
    public string Value { get; } = value;

    // Optional: Unit of measure (e.g., "Bar", "Celsius", "RPM")
    public string UnitOfMeasure { get; } = unitOfMeasure;

    public static Result<EquipmentCapability> Create(string name, string value, string unitOfMeasure)
    {
        var equipmentCapability = new EquipmentCapability(name, value, unitOfMeasure);

        return equipmentCapability;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Value;
        yield return UnitOfMeasure;
    }
}

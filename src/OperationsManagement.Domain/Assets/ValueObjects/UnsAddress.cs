using OperationsManagement.Domain._Shared;

namespace OperationsManagement.Domain.Assets.ValueObjects;

public sealed class UnsAddress(string enterprise, string site, string area, string processCell, string unit) : ValueObject
{
    public string Enterprise { get; init; } = enterprise;   // Level 4/5 root
    public string Site { get; init; } = site;
    public string Area { get; init; } = area;
    public string ProcessCell { get; init; } = processCell; // ISA-88 "ProcessCell/Line"
    public string Unit { get; init; } = unit;             // ISA-88 "Unit/Machine"

    public static Result<UnsAddress> Create(string enterprise, string site, string area, string processCell, string unit)
    {
        var unsPath = new UnsAddress(enterprise, site, area, processCell, unit);

        return unsPath;
    }

    // Formats the path for MQTT topics
    public string ToTopic() => $"{Site}/{Area}/{ProcessCell}/{Unit}";

    // Formats for the Command branch
    public string ToCommandTopic(string command) => $"{ToTopic()}/Cmd/{command}";

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}

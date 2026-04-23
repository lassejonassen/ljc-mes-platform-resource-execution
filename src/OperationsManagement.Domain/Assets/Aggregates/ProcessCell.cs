using OperationsManagement.Domain.Assets.Errors;
using UnitEnt = OperationsManagement.Domain.Assets.Entities.Unit;

namespace OperationsManagement.Domain.Assets.Aggregates;

public sealed class ProcessCell : AggregateRoot
{
    private ProcessCell() { }
    private ProcessCell(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid AreaId { get; private set; }

    private readonly List<UnitEnt> _units = [];
    public IReadOnlyCollection<UnitEnt> Units => _units.AsReadOnly();

    public static Result<ProcessCell> Create(string name, string? description, Guid areaId, DateTime utcNow)
    {

        var area = new ProcessCell(utcNow)
        {
            Name = name,
            Description = description,
            AreaId = areaId,
        };

        return Result.Success(area);
    }

    public Result Update(string name, string? description)
    {
        Name = name;
        Description = description;

        return Result.Success();
    }

    public Result AddUnit(string name, string? description, Guid? processSegmentId, DateTime utcNow)
    {
        var unit = _units.FirstOrDefault(x => x.Name == name);
        if (unit is null)
            return Result.Failure(ProcessCellErrors.UnitAlreadyExistsOnProcessCell);

        var newUnit = UnitEnt.Create(Id, name, description, processSegmentId, utcNow);

        if (newUnit.IsFailure)
            return Result.Failure(newUnit.Error);

        _units.Add(unit);

        return Result.Success();
    }

    public Result UpdateUnit(
        Guid unitId,
        string name,
        string? description,
        Guid? processSegmentId)
    {
        var unit = _units.FirstOrDefault(x => x.Id == unitId);
        if (unit is null)
            return Result.Failure(ProcessCellErrors.UnitNotFound);

        var updateResult = unit.Update(name, description, processSegmentId);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        return Result.Success();
    }

    public Result RemoveUnit(Guid unitId)
    {
        var unit = _units.FirstOrDefault(x => x.Id == unitId);
        if (unit is null)
            return Result.Failure(ProcessCellErrors.UnitNotFound);

        _units.Remove(unit);

        return Result.Success();
    }
}

using ResourceExecution.Domain._Shared;
using ResourceExecution.Domain.ResourceManagement.Entities;
using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.SharedKernel;

namespace ResourceExecution.Domain.ResourceManagement.Aggregates;

public sealed class WorkCenter : AggregateRoot
{
    private WorkCenter() { }
    private WorkCenter(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private readonly List<WorkUnit> _workUnits = [];
    public IReadOnlyCollection<WorkUnit> WorkUnits => _workUnits.AsReadOnly();

    public static Result<WorkCenter> Create(string name, string? description, DateTime utcNow)
    {
        var area = new WorkCenter(utcNow)
        {
            Name = name,
            Description = description,
        };

        return Result.Success(area);
    }

    public Result Update(string name, string? description)
    {
        Name = name;
        Description = description;

        return Result.Success();
    }

    public Result AddWorkUnit(string name, string? description, Guid equipmentClassId, DateTime utcNow)
    {
        var unit = _workUnits.FirstOrDefault(x => x.Name == name);
        if (unit is null)
            return Result.Failure(WorkCenterErrors.WorkUnitAlreadyExists);

        var newUnit = WorkUnit.Create(name, description, Id, equipmentClassId, utcNow);

        if (newUnit.IsFailure)
            return Result.Failure(newUnit.Error);

        _workUnits.Add(unit);

        return Result.Success();
    }

    public Result UpdateUnit(
        Guid workUnitId,
        string name,
        string? description,
        Guid equipmentClassId)
    {
        var workUnit = _workUnits.FirstOrDefault(x => x.Id == workUnitId);
        if (workUnit is null)
            return Result.Failure(WorkCenterErrors.WorkUnitNotFound);

        var updateResult = workUnit.Update(name, description, equipmentClassId);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        return Result.Success();
    }

    public Result RemoveUnit(Guid workUnitId)
    {
        var unit = _workUnits.FirstOrDefault(x => x.Id == workUnitId);
        if (unit is null)
            return Result.Failure(WorkCenterErrors.WorkUnitNotFound);

        _workUnits.Remove(unit);

        return Result.Success();
    }
}

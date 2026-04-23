using OperationsManagement.Domain.Assets.Aggregates;
using OperationsManagement.Domain.Assets.Enums;
using OperationsManagement.Domain.Assets.ValueObjects;

namespace OperationsManagement.Domain.Assets.Entities;

public sealed class Unit : AggregateRoot
{
    private Unit() { }
    private Unit(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid ProcessCellId { get; private set; }
    public Guid? ProcessSegmentId { get; private set; }
    public UnsAddress? UnsAddress { get; private set; } = null!;
    public UnitStatus Status { get; private set; }

    public ProcessCell ProcessCell { get; } = null!;

    public bool CanExecute(Guid processSegmentId)
        => ProcessSegmentId == processSegmentId && Status == UnitStatus.Idle;

    public static Result<Unit> Create(
        Guid processCellId,
        string name, 
        string? description, 
        Guid? processSegmentId,
        DateTime utcNow)
    {
        var unit = new Unit(utcNow)
        {
            Name = name,
            Description = description,
            ProcessCellId = processCellId,
            ProcessSegmentId = processSegmentId,
        };

        return Result.Success(unit);
    }

    public Result Update(string name, string? description, Guid? processSegmentId)
    {
        Name = name;
        Description = description;
        ProcessSegmentId = processSegmentId;

        return Result.Success();
    }

    public Result SetUnsAddress(string enterprise, string site, string area, string processCell, string unit)
    {
        var unsAddress = UnsAddress.Create(
            enterprise, site, area, processCell, unit);

        if (unsAddress.IsFailure)
            return Result.Failure(unsAddress.Error);

        UnsAddress = unsAddress.Value;

        return Result.Success(unsAddress.Value);
    }
}

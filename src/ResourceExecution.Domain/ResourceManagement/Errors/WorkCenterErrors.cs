using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.SharedKernel;

namespace ResourceExecution.Domain.ResourceManagement.Errors;

public static class WorkCenterErrors
{
    private const string Prefix = nameof(WorkCenter);

    public static readonly Error NotFound
        = new($"{Prefix}.NotFound", "The process cell with the specified ID was not found.", ErrorType.Failure);

    public static readonly Error WorkUnitAlreadyExists
        = new($"{Prefix}.WorkUnitAlreadyExists", "A Work Unit with the specified name already exists on the process cell", ErrorType.Failure);

    public static readonly Error WorkUnitNotFound
        = new($"{Prefix}.WorkUnitNotFound", "A Work Unit with the specified ID was not found", ErrorType.Failure);

    public static readonly Error AlreadyExists
        = new($"{Prefix}.AlreadyExists", "The Work Center with the specified name already exists", ErrorType.Failure);
}

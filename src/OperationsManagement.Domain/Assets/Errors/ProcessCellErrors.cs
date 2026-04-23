using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Errors;

public static class ProcessCellErrors
{
    private const string Prefix = nameof(ProcessCell);

    public static readonly Error NotFound
        = new($"{Prefix}.NotFound", "The process cell with the specified ID was not found.", ErrorType.Failure);

    public static readonly Error UnitAlreadyExistsOnProcessCell
        = new($"{Prefix}.UnitAlreadyExistsOnProcessCell", "A unit with the specified name already exists on the process cell", ErrorType.Failure);

    public static readonly Error UnitNotFound
        = new($"{Prefix}.UnitNotFound", "A unit with the specified ID was not found", ErrorType.Failure);
}

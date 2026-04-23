using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Errors;

public static class AreaErrors
{
    private const string Prefix = nameof(Area);

    public static readonly Error NotFound
        = new($"{Prefix}.NotFound", "The area with the specified ID was not found.", ErrorType.Failure);
}

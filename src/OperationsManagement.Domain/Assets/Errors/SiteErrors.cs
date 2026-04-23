using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Errors;

public static class SiteErrors
{
    private const string Prefix = nameof(Site);

    public static readonly Error NotFound
        = new($"{Prefix}.NotFound", "The site with the specified ID was not found.", ErrorType.NotFound);

    public static readonly Error AlreadyExists
        = new($"{Prefix}.AlreadyExists", "The site with the specified name already exists.", ErrorType.Failure);
}

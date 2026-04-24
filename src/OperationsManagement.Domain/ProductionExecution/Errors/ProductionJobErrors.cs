using ResourceExecution.Domain.ProductionExecution.Aggregates;
using ResourceExecution.SharedKernel;

namespace ResourceExecution.Domain.ProductionExecution.Errors;

public static class ProductionJobErrors
{
    private const string Prefix = nameof(ProductionJob);

    public static readonly Error OperationExists
    = new($"{Prefix}.OperationExists", "The operation already exists", ErrorType.Failure);
}

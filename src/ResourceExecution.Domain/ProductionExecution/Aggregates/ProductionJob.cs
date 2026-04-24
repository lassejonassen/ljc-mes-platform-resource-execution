using ResourceExecution.Domain._Shared;
using ResourceExecution.Domain.ProductionExecution.Entities;
using ResourceExecution.Domain.ProductionExecution.Enums;
using ResourceExecution.Domain.ProductionExecution.Errors;
using ResourceExecution.SharedKernel;

namespace ResourceExecution.Domain.ProductionExecution.Aggregates;

public sealed class ProductionJob : AggregateRoot
{
    private ProductionJob() { }
    private ProductionJob(DateTime utcNow) : base(utcNow) { }

    public string OrderNumber { get; private set; } = string.Empty;
    public string ProductCode { get; private set; } = string.Empty;
    public int TargetQuantity { get; private set; }
    public JobStatus Status { get; private set; }

    public Guid PlannedWorkCenterId { get; private set; }
    public Guid? AssignedWorkUnitId { get; private set; }

    private readonly List<JobOperation> _operations = [];
    public IReadOnlyCollection<JobOperation> Operations => _operations.AsReadOnly();

    public static Result<ProductionJob> Create(string orderNumber, string productCode, int targetQuantity, Guid plannedWorkCenterId, Guid? assignedWorkUnitId, DateTime utcNow)
    {
        var job = new ProductionJob(utcNow)
        {
            OrderNumber = orderNumber,
            ProductCode = productCode,
            TargetQuantity = targetQuantity,
            Status = JobStatus.Pending,
            PlannedWorkCenterId = plannedWorkCenterId,
            AssignedWorkUnitId = assignedWorkUnitId,
        };

        return Result.Success(job);
    }

    public Result AddOperation(string operationCode, int sequence, Guid requiredEquipmentClassId, DateTime utcNow)
    {
        bool exists = _operations.Any(x => x.OperationCode == operationCode);
        if (exists)
            return Result.Failure(ProductionJobErrors.OperationExists);

        var operation = JobOperation.Create(Id, operationCode, sequence, requiredEquipmentClassId, utcNow);

        if (operation.IsFailure)
            return Result.Failure(operation.Error);

        _operations.Add(operation.Value);

        return Result.Success(operation);
    }
}

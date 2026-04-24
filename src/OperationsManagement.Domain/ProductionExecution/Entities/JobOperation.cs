using ResourceExecution.Domain._Shared;
using ResourceExecution.Domain.ProductionExecution.Enums;
using ResourceExecution.Domain.ResourceManagement.ValueObjects;
using ResourceExecution.SharedKernel;

namespace ResourceExecution.Domain.ProductionExecution.Entities;

public sealed class JobOperation : Entity
{
    private JobOperation() { }
    private JobOperation(DateTime utcNow) : base(utcNow) { }

    public Guid ProductionJobId { get; private set; }
    public string OperationCode { get; private set; } = string.Empty;
    public int Sequence { get; private set; }
    public Guid RequiredEquipmentClassId { get; private set; }
    public OperationStatus Status { get; private set; }

    private readonly List<EquipmentCapability> _requiredCapabilities = [];
    public IReadOnlyCollection<EquipmentCapability> RequiredCapabilities => _requiredCapabilities;

    public static Result<JobOperation> Create(Guid productionJobId, string operationCode, int sequence, Guid requiredEquipmentClassId, DateTime utcNow)
    {
        var jobOperation = new JobOperation(utcNow)
        {
            ProductionJobId = productionJobId,
            OperationCode = operationCode,
            Sequence = sequence,
            RequiredEquipmentClassId = requiredEquipmentClassId,
            Status = OperationStatus.Pending
        };

        return Result.Success(jobOperation);
    }

    public void AddRequiredCapability(EquipmentCapability capability)
    {
        if (Status != OperationStatus.Pending)
            throw new InvalidOperationException("Cannot modify requirements once operation has started.");

        _requiredCapabilities.Add(capability);
    }

    public void MarkAsReady() => Status = OperationStatus.Ready;

    public void Start() => Status = OperationStatus.InProgress;

    public void Complete() => Status = OperationStatus.Completed;

    public void Skip() => Status = OperationStatus.Skipped;
}

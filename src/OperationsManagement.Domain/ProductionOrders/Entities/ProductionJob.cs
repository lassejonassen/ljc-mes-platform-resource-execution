using OperationsManagement.Domain._Shared;
using OperationsManagement.Domain.Assets.ValueObjects;
using OperationsManagement.Domain.ProductionOrders.Enums;

namespace OperationsManagement.Domain.ProductionOrders.Entities;

public sealed class ProductionJob : Entity
{
    public Guid UnitId { get; private set; }
    public UnsAddress Address { get; private set; } = null!;
    public JobStatus Status { get; private set; }

    private readonly List<ProductionJobParameter> _jobs = [];
    public IReadOnlyCollection<ProductionJobParameter> Jobs => _jobs.AsReadOnly();
}

using ResourceExecution.Domain._Shared;
using ResourceExecution.Domain.ProductionExecution.Aggregates;

namespace ResourceExecution.Domain.ProductionExecution.Repositories;

public interface IProductionJobRepository : IRepository<ProductionJob>
{
    Task<IReadOnlyCollection<ProductionJob>> GetAllAsync( CancellationToken cancellationToken);
    Task<ProductionJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

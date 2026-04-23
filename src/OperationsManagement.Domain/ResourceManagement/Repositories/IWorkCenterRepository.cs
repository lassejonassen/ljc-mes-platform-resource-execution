using ResourceExecution.Domain._Shared;
using ResourceExecution.Domain.ResourceManagement.Aggregates;

namespace ResourceExecution.Domain.ResourceManagement.Repositories;

public interface IWorkCenterRepository : IRepository<WorkCenter>
{
    Task<IReadOnlyCollection<WorkCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
}

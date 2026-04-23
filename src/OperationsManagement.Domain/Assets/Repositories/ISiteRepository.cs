using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Repositories;

public interface ISiteRepository : IRepository<Site>
{
    Task<IReadOnlyCollection<Site>> GetAllAsync(CancellationToken cancellationToken);
    Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
}

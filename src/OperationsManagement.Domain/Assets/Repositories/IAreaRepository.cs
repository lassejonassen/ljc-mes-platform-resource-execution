using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Repositories;

public interface IAreaRepository : IRepository<Area>
{
    Task<IReadOnlyCollection<Area>> GetAllAsync(Guid siteId, CancellationToken cancellationToken);
    Task<Area?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(Guid siteId, string name, CancellationToken cancellationToken);
}

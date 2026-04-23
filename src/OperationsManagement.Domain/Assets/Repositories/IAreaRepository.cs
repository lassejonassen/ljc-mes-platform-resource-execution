using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Repositories;

public interface IAreaRepository : IRepository<Area>
{
    Task<IReadOnlyCollection<Area>> GetAllAsync(CancellationToken cancellationToken);
    Task<Area?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

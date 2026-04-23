using OperationsManagement.Domain.Assets.Aggregates;

namespace OperationsManagement.Domain.Assets.Repositories;

public interface IProcessCellRepository : IRepository<ProcessCell>
{
    Task<IReadOnlyCollection<ProcessCell>> GetAllAsync(Guid areaId, CancellationToken cancellationToken);
    Task<ProcessCell?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

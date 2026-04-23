using ResourceExecution.Domain._Shared;
using ResourceExecution.Domain.ResourceManagement.Aggregates;

namespace ResourceExecution.Domain.ResourceManagement.Repositories;

public interface IEquipmentClassRepository : IRepository<EquipmentClass>
{
    Task<IReadOnlyCollection<EquipmentClass>> GetAllAsync( CancellationToken cancellationToken);
    Task<EquipmentClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
}

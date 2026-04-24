using Microsoft.EntityFrameworkCore;
using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.Infrastructure.Persistence.DbContexts;

namespace ResourceExecution.Infrastructure.Persistence.Repositories;

internal sealed class EquipmentClassRepository(ApplicationDbContext context)
     : Repository<EquipmentClass>(context), IEquipmentClassRepository
{
    public async Task<IReadOnlyCollection<EquipmentClass>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<EquipmentClass>()
            .ToListAsync(cancellationToken);
    }

    public async Task<EquipmentClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<EquipmentClass>()
            .Include(x => x.StandardCapabilities)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await DbContext.Set<EquipmentClass>()
            .AnyAsync(x => x.Name == name, cancellationToken);
    }
}

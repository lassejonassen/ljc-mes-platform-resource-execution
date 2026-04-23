using Microsoft.EntityFrameworkCore;
using OperationsManagement.Domain.ProductionExecution.Repositories;
using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.Infrastructure.Persistence.DbContexts;

namespace ResourceExecution.Infrastructure.Persistence.Repositories;

internal sealed class ProcessCellRepository(ApplicationDbContext context)
     : Repository<WorkCenter>(context), IEquipmentClassRepository
{
    public async Task<IReadOnlyCollection<WorkCenter>> GetAllAsync(Guid areaId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<WorkCenter>()
            .Where(x => x.AreaId == areaId)
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<WorkCenter>()
            .Include(x => x.Units)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}

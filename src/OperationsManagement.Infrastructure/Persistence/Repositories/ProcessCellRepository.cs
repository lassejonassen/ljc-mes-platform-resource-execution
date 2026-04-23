using Microsoft.EntityFrameworkCore;
using OperationsManagement.Domain.Assets.Aggregates;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.Infrastructure.Persistence.DbContexts;

namespace OperationsManagement.Infrastructure.Persistence.Repositories;

internal sealed class ProcessCellRepository(ApplicationDbContext context)
     : Repository<ProcessCell>(context), IProcessCellRepository
{
    public async Task<IReadOnlyCollection<ProcessCell>> GetAllAsync(Guid areaId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<ProcessCell>()
            .Where(x => x.AreaId == areaId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProcessCell?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<ProcessCell>()
            .Include(x => x.Units)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}

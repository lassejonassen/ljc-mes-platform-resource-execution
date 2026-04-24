using Microsoft.EntityFrameworkCore;
using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.Infrastructure.Persistence.DbContexts;

namespace ResourceExecution.Infrastructure.Persistence.Repositories;

internal sealed class WorkCenterRepository(ApplicationDbContext context)
     : Repository<WorkCenter>(context), IWorkCenterRepository
{
    public async Task<IReadOnlyCollection<WorkCenter>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<WorkCenter>()
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<WorkCenter>()
            .Include(x => x.WorkUnits)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await DbContext.Set<WorkCenter>()
             .AnyAsync(x => x.Name == name, cancellationToken);
    }
}

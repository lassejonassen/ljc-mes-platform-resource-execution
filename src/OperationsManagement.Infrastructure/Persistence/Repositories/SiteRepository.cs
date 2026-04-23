using Microsoft.EntityFrameworkCore;
using OperationsManagement.Domain.Assets.Aggregates;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.Infrastructure.Persistence.DbContexts;

namespace OperationsManagement.Infrastructure.Persistence.Repositories;

internal sealed class SiteRepository(ApplicationDbContext context)
     : Repository<Site>(context), ISiteRepository
{
    public async Task<IReadOnlyCollection<Site>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Site>().ToListAsync(cancellationToken);
    }

    public async Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Site>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await DbContext.Set<Site>().AnyAsync(x => x.Name == name, cancellationToken);
    }
}

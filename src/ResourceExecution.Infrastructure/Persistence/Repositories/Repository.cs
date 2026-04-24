using Microsoft.EntityFrameworkCore;
using ResourceExecution.Domain._Shared;

namespace ResourceExecution.Infrastructure.Persistence.Repositories;

internal abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity>
    where TEntity : Entity
{
    protected DbContext DbContext => context;

    public TEntity Add(TEntity entity)
    {
        return context.Set<TEntity>().Add(entity).Entity;
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
}

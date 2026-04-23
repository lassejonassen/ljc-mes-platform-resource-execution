using Microsoft.EntityFrameworkCore;
using OperationsManagement.Domain._Shared;

namespace OperationsManagement.Infrastructure.Persistence.Repositories;

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

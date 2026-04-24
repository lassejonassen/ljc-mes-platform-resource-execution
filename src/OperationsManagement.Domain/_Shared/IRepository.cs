namespace ResourceExecution.Domain._Shared;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    TEntity Add(TEntity entity);
    void Delete(TEntity entity);
}
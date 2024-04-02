namespace TodoList.Repository;

public interface ITaskRepository<TEntity> : IRepository<TEntity>
{
  public Task<IEnumerable<TEntity>> GetIncludes();
}
namespace TodoList.Repository;

public interface ICategoryRepository<TEntity> : IRepository<TEntity>
{
  public Task<IEnumerable<TEntity>> GetParents();
}
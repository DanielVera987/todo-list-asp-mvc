namespace TodoList.Service;

public interface ICategoryService<TEntity> : IService<TEntity>
{
  public Task<IEnumerable<TEntity>> List();
  public Task<IEnumerable<TEntity>> GetParents();
  public Task<IEnumerable<TEntity>> GetWithTask();
}
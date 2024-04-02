namespace TodoList.Repository;

public interface IRepository<TEntity>
{
  public Task<TEntity> Find(uint id);
  public Task<IEnumerable<TEntity>> GetAll();
  public Task<TEntity> Save(TEntity entity);
  public Task<TEntity> Update(TEntity entity);
  public Task<bool> Delete(TEntity entity);
}
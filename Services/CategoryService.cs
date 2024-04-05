using TodoList.Models;
using TodoList.Repository;

namespace TodoList.Service;

public class CategoryService : ICategoryService<Category>
{
  private ICategoryRepository<Category> _repository;
  
  public CategoryService(ICategoryRepository<Category> repository)
  {
    _repository = repository;
  }

  public async Task<dynamic> Index()
  {
    return await this.List();
  }

  public async Task<IEnumerable<Category>> List()
  {
    return await _repository.GetAll();
  }

  public async Task<IEnumerable<Category>> GetParents()
  {
    return await _repository.GetParents();
  }

  public async Task<IEnumerable<Category>> GetWithTask()
  {
    return await _repository.GetWithTask();
  }

  public async Task<dynamic> Create()
  {
    return await this.GetParents();
  }

  public async Task<dynamic> Create(Category entity)
  {
    return await _repository.Save(entity);
  }

  public async Task<dynamic> Edit(uint id)
  {
    return await _repository.Find(id);
  }

  public async Task<dynamic> Edit(Category entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<dynamic> Delete(uint id)
  {
    var category = await _repository.Find(id);

    if (category == null) {
      return null;
    }

    return await _repository.Delete(category);
  }
}
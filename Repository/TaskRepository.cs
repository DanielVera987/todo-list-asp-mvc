using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Models.ViewModel;
using ModelTask = TodoList.Models.Task;

namespace TodoList.Repository;

class TaskRepository : ITaskRepository<ModelTask>
{
  private ApplicationDbContext _context;

  public TaskRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<ModelTask> Find(uint id) => await _context.Tasks.FindAsync(id);

  public async Task<IEnumerable<ModelTask>> GetAll() =>
    await _context.Tasks.ToListAsync();

  public async Task<IEnumerable<ModelTask>> GetIncludes()
  {
    return await _context.Tasks.Include(p => p.Category).ToListAsync();
  }

  public async Task<IEnumerable<ModelTask>> GetAllWithRelationship() =>
    await _context.Tasks.Include(p => p.Category).ToListAsync();
    
  public async Task<ModelTask> Save(ModelTask model)
  {
    if (model == null) {
      return null;
    }

    _context.Tasks.Add(model);
    await _context.SaveChangesAsync();

    return model;
  }

  public async Task<ModelTask> Update(ModelTask model)
  {
    _context.Tasks.Update(model);
    await _context.SaveChangesAsync();

    return model;
  }

  public async Task<bool> Delete(ModelTask model)
  {
    if (model == null)
    {
      return false;
    }
  
    _context.Tasks.Remove(model);
    await _context.SaveChangesAsync();

    return true;
  }
}
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Repository;

public class CategoryRepository : ICategoryRepository<Category>
{
  private ApplicationDbContext _context;

  public CategoryRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Category> Find(uint id) => 
    await _context.Categories.FindAsync(id);

  public async Task<IEnumerable<Category>> GetAll() => 
    await _context.Categories.ToListAsync();

  public async Task<IEnumerable<Category>> GetParents()
  {
    return await _context.Categories.Where(c => c.ParentId == null).ToListAsync();
  }

  public async Task<Category> Save(Category entity)
  {
    _context.Categories.Add(entity);
    await _context.SaveChangesAsync();

    return entity;
  }

  public async Task<Category> Update(Category entity)
  {
    _context.Categories.Update(entity);
    await _context.SaveChangesAsync();

    return entity;
  }

  public async Task<bool> Delete(Category entity)
  {
    if (entity == null) 
    {
      return false;
    }

    _context.Categories.Remove(entity);
    await _context.SaveChangesAsync();

    return true;
  }
}
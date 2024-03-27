using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Controllers;

public class CategoryController : Controller
{
  private ApplicationDbContext _context;

  public CategoryController(ApplicationDbContext context)
  { 
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    List<Category> categories = await _context.Categories.ToListAsync(); 
    return View(categories);
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    List<Category> parents = await _context.Categories.Where(c => c.ParentId == null).ToListAsync();
    ViewBag.Parents = parents;
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(Category category)
  {
    if (category == null) 
    {
      return NotFound();
    }

    _context.Categories.Add(category);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
  }
}
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

  [HttpGet]
  public async Task<IActionResult> Edit(uint? id)
  {
    if (id == null) return NotFound();

    Category category = await _context.Categories.FindAsync(id);
    List<Category> parents = await _context.Categories.Where(c => c.ParentId == null).ToListAsync();
    ViewBag.Parents = parents;

    if (category == null) return NotFound();

    return View(category);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(Category category)
  {
    if (category == null) return NotFound();

    _context.Categories.Update(category);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
  }
}
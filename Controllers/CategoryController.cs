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

  public async Task<IActionResult> Index()
  {
    List<Category> categories = await _context.Categories.ToListAsync(); 
    return View(categories);
  }
}
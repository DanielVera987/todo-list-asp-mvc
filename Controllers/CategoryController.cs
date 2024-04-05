using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Repository;
using TodoList.Service;

namespace TodoList.Controllers;

public class CategoryController : Controller
{
  private ICategoryService<Category> _service;

  public CategoryController(ICategoryService<Category> service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var categories = await _service.Index(); 
    return View(categories);
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    var parents = await _service.Create();
    ViewBag.Parents = parents;
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(Category category)
  {
    if (category == null) return NotFound();

    await _service.Create(category);

    return RedirectToAction(nameof(Index));
  }

  [HttpGet]
  public async Task<IActionResult> Edit(uint id)
  {
    if (id == null) return NotFound();

    Category category = await _service.Edit(id);
    var parents = await _service.GetParents();
    ViewBag.Parents = parents;

    if (category == null) return NotFound();

    return View(category);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(Category category)
  {
    if (category == null) return NotFound();

    await _service.Edit(category);

    return RedirectToAction(nameof(Index));
  }

  [HttpGet]
  public async Task<IActionResult> Delete(Category category)
  {
    if (category == null) return NotFound();

    await _service.Delete(category.Id);

    return RedirectToAction(nameof(Index));
  }
}
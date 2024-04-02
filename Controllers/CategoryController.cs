using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList.Controllers;

public class CategoryController : Controller
{
  private ICategoryRepository<Category> _repository;

  public CategoryController(ICategoryRepository<Category> repository)
  {
    _repository = repository;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var categories = await _repository.GetAll(); 
    return View(categories);
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    var parents = await _repository.GetParents();
    ViewBag.Parents = parents;
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(Category category)
  {
    if (category == null) return NotFound();

    await _repository.Save(category);

    return RedirectToAction(nameof(Index));
  }

  [HttpGet]
  public async Task<IActionResult> Edit(uint id)
  {
    if (id == null) return NotFound();

    Category category = await _repository.Find(id);
    var parents = await _repository.GetParents();
    ViewBag.Parents = parents;

    if (category == null) return NotFound();

    return View(category);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(Category category)
  {
    if (category == null) return NotFound();

    await _repository.Update(category);

    return RedirectToAction(nameof(Index));
  }

  [HttpGet]
  public async Task<IActionResult> Delete(Category category)
  {
    if (category == null) return NotFound();

    await _repository.Delete(category);

    return RedirectToAction(nameof(Index));
  }
}
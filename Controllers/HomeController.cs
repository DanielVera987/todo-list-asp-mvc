using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoList.Models;
using TodoList.Models.ViewModel;
using TodoList.Service;

namespace TodoList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ICategoryService<Category> _categoryService;
    private IService<ViewModelTask> _service;

    public HomeController(
      ILogger<HomeController> logger, 
      ICategoryService<Category> categoryService,
      IService<ViewModelTask> service
    )
    {
        _logger = logger;
        _categoryService = categoryService;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var viewModel = await _service.Index();
      return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      var categories = await _service.Create();
      ViewBag.Categories = categories;
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ViewModelTask model)
    {
      var task = await _service.Create(model);
      if (task == null) return NotFound();

      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("tarea/editar/{id}")]
    public async Task<IActionResult> Edit(uint id)
    {
      var viewModel = await _service.Edit(id);

      if (viewModel == null) return NotFound();

      ViewData["Categories"] = await _categoryService.List();

      return View(viewModel);
    }

    [HttpPost]
    [Route("tarea/editar/{id}")]
    public async Task<IActionResult> Edit(uint? id, ViewModelTask model) {
      var task = await _service.Edit(model);
      
      if (task == null) {
        return NotFound();
      }

      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(uint id)
    {
      var task = await _service.Delete(id);

      if (task == null) {
        return NotFound();
      }

      return RedirectToAction(nameof(Index));
    }
}

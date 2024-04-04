using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoList.Models;
using TodoList.Models.ViewModel;
using TodoList.Repository;
using TodoList.Service;
using ModelTask = TodoList.Models.Task;

namespace TodoList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ITaskRepository<ModelTask> _repository;
    private ICategoryRepository<Category> _categoryRepository;
    private IService<ViewModelTask> _service;

    public HomeController(
      ILogger<HomeController> logger, 
      ITaskRepository<ModelTask> repository,
      ICategoryRepository<Category> categoryRepository,
      IService<ViewModelTask> service
    )
    {
        _logger = logger;
        _repository = repository;
        _categoryRepository = categoryRepository;
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

      ViewData["Categories"] = await _categoryRepository.GetAll();

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

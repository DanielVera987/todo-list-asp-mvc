using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoList.Models;
using TodoList.Models.ViewModel;
using TodoList.Repository;
using ModelTask = TodoList.Models.Task;

namespace TodoList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ITaskRepository<ModelTask> _repository;
    private ICategoryRepository<Category> _categoryRepository;

    public HomeController(
      ILogger<HomeController> logger, 
      ITaskRepository<ModelTask> repository,
      ICategoryRepository<Category> categoryRepository  
    )
    {
        _logger = logger;
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var tasks = await _repository.GetIncludes();
      var viewModel = new List<ViewModelTask>();

      foreach (var task in tasks) {
        ViewModelTask viewModelTask = new ViewModelTask{
          Id = task.Id,
          Title = task.Title,
          CategoryId = task.CategoryId,
          Description = task.Description,
          StartDate = task.StartDate,
          EndDate = task.EndDate,
          Category = task.Category
        };

        viewModel.Add(viewModelTask);
      }

      return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.Categories = await _categoryRepository.GetAll();
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ViewModelTask model)
    {
      if (model == null) {
        return NotFound();
      }

      var task = new TodoList.Models.Task
      {
        CategoryId = model.CategoryId,
        Title = model.Title,
        Description = model.Description,
        StartDate = model.StartDate,
        EndDate = model.EndDate
      };

      await _repository.Save(task);

      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("tarea/editar/{id}")]
    public async Task<IActionResult> Edit(uint id)
    {
      var task = await _repository.Find(id);

      if (task == null) {
        Response.StatusCode = 404;
        return NotFound();
      }

      var viewModel = new ViewModelTask{
        Title = task.Title,
        CategoryId = task.CategoryId,
        Description = task.Description,
        StartDate = task.StartDate,
        EndDate = task.EndDate
      };

      ViewData["Categories"] = await _categoryRepository.GetAll();

      return View(viewModel);
    }

    [HttpPost]
    [Route("tarea/editar/{id}")]
    public async Task<IActionResult> Edit(uint? id, ViewModelTask model) {
      if (model == null) {
        return NotFound();
      }

      var task = new TodoList.Models.Task(){
        Id = model.Id,
        CategoryId = model.CategoryId,
        Title = model.Title,
        Description = model.Description,
        StartDate = model.StartDate,
        EndDate = model.EndDate
      };

      await _repository.Update(task);

      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(uint Id)
    {
      if (Id == null) {
        return NotFound();
      }

      var task = await _repository.Find(Id);
      _repository.Delete(task);

      return RedirectToAction(nameof(Index));
    }
}

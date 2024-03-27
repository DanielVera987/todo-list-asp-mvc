using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoList.Models;
using TodoList.Models.ViewModel;

namespace TodoList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var tasks = await _context.Tasks.Include(c => c.Category).ToListAsync();
      var viewModel = new List<ViewModelTask>();

      foreach (var task in tasks) {
        ViewModelTask viewModelTask = new ViewModelTask{
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
      ViewBag.Categories = await _context.Categories.ToListAsync();
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ViewModelTask model)
    {
      if (ModelState.IsValid) {
        var task = new TodoList.Models.Task
        {
          Id = model.Id,
          CategoryId = model.CategoryId,
          Title = model.Title,
          Description = model.Description,
          StartDate = model.StartDate,
          EndDate = model.EndDate
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("tarea/editar/{id}")]
    public async Task<IActionResult> Edit(uint id)
    {
      var task = await _context.Tasks.FindAsync(id);

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

      ViewData["Categories"] = await _context.Categories.ToListAsync();

      return View(viewModel);
    }

    [HttpPost]
    [Route("tarea/editar/{id}")]
    public async Task<IActionResult> Edit(uint id, ViewModelTask model) {
      if (ModelState.IsValid) {
        var task = new TodoList.Models.Task(){
          Id = model.Id,
          CategoryId = model.CategoryId,
          Title = model.Title,
          Description = model.Description,
          StartDate = model.StartDate,
          EndDate = model.EndDate
        };

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(uint? Id)
    {
      if (Id == null) {
        return NotFound();
      }

      var task = await _context.Tasks.FindAsync(Id);
      _context.Tasks.Remove(task);
      await _context.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }
}

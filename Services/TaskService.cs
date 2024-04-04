using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Models.ViewModel;
using TodoList.Repository;
using ModelTask = TodoList.Models.Task;

namespace TodoList.Service;

public class TaskService : IService<ViewModelTask>
{
  private ITaskRepository<ModelTask> _repository;
  private ICategoryRepository<Category> _categoryRepository;

  public TaskService(ITaskRepository<ModelTask> repository, ICategoryRepository<Category> categoryRepository)
  {
    _repository = repository;
    _categoryRepository = categoryRepository;
  }

  public async Task<dynamic> Index()
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

    return viewModel;
  }

  public async Task<dynamic> Create()
  {
    return await _categoryRepository.GetAll();
  }

  public async Task<dynamic> Create(ViewModelTask entity)
  {
    if (entity == null) {
        return null;
      }

      var task = new TodoList.Models.Task
      {
        CategoryId = entity.CategoryId,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate
      };

      await _repository.Save(task);

      return task;
  }

  public async Task<dynamic> Edit(uint id)
  {
    var task = await _repository.Find(id);

    if (task == null) {
      return null;
    }

    var viewModel = new ViewModelTask{
      Title = task.Title,
      CategoryId = task.CategoryId,
      Description = task.Description,
      StartDate = task.StartDate,
      EndDate = task.EndDate
    };

    return viewModel;
  }

  public async Task<dynamic> Edit(ViewModelTask entity)
  {
    if (entity == null) {
        return null;
      }

      var task = new TodoList.Models.Task(){
        Id = entity.Id,
        CategoryId = entity.CategoryId,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate
      };

      await _repository.Update(task);
      return task;
  }

  public async Task<dynamic> Delete(uint id)
  {
    var task = await _repository.Find(id);

    if (task == null) 
    {
      return false;
    }

    await _repository.Delete(task);
    return task;
  }
}
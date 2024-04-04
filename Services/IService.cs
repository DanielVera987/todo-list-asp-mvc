using Microsoft.AspNetCore.Mvc;

namespace TodoList.Service;

public interface IService<TViewModel>
{
  public Task<dynamic> Index();
  public Task<dynamic> Create();
  public Task<dynamic> Create(TViewModel entity);
  public Task<dynamic> Edit(uint id);
  public Task<dynamic> Edit(TViewModel entity);
  public Task<dynamic> Delete(uint id);
}
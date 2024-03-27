using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

public class ErrorPageController : Controller 
{
  [Route("ErrorPage/{statusCode}")]
  public IActionResult Index(int statusCode)
  {
    switch(statusCode) 
    {
      case 404:
        ViewBag.Error = "Not found";
        break;
    }

    return View("ErrorPage");
  }
}
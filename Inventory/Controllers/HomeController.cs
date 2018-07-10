using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}

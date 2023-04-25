using Microsoft.AspNetCore.Mvc;

namespace WebBDS.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("Index");
    }
}
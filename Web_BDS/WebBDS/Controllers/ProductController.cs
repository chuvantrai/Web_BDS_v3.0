using Microsoft.AspNetCore.Mvc;

namespace WebBDS.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("/Views/Public/ProductsList.cshtml");
    }
}
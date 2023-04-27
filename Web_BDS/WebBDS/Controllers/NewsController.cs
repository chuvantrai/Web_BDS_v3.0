using Microsoft.AspNetCore.Mvc;

namespace WebBDS.Controllers;

public class NewsController : Controller
{
    // GET
    //  public IActionResult Index()
    // {
    //     return View("/Views/Public/NewsDetail.cshtml");
    // }
    [HttpGet]
    public IActionResult NewsDetail()
    {
        return View("/Views/Public/NewsDetail.cshtml");
    }
    [HttpGet]
    public IActionResult NewsList()
    {
        return View("/Views/Public/NewsList.cshtml");
    }
}
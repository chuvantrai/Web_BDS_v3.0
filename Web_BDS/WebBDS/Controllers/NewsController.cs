using Microsoft.AspNetCore.Mvc;
using WebBDS.Extensions;
using WebBDS.Models;

namespace WebBDS.Controllers;

public class NewsController : AbstractController
{
    // private readonly Bds_CShapContext _bdsCShapContext;
    // public NewsController(IJwtTokenHandler jwtTokenHandler,Bds_CShapContext bdsCShapContext) : base(jwtTokenHandler)
    // {
    //     _bdsCShapContext = bdsCShapContext;
    // }

    [HttpGet]
    public IActionResult NewsDetail()
    {
        // if (!CheckRoleUser( new []{"Admin"} ,false))
        // {
        //     return View("Error");
        // }
        return View("/Views/Public/NewsDetail.cshtml");
    }
    [HttpGet]
    public IActionResult NewsList()
    {
        return View("/Views/Public/NewsList.cshtml");
    }
}
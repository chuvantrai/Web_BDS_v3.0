using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBDS.Commons;
using WebBDS.Models;
using WebBDS.ResponseModels;

namespace WebBDS.Controllers;

public class HomeController : Controller
{
    private readonly Bds_CShapContext _bdsCShapContext;
    public HomeController(Bds_CShapContext bdsCShapContext)
    {
        _bdsCShapContext = bdsCShapContext;
    }

    [HttpGet]
    public ActionResult Index()
    {
        return View("Index");
    }
    [HttpGet]
    public async Task<ActionResult> HomeData()
    {
        var homeResponse = new HomeResponse();
        homeResponse.Top3News = await _bdsCShapContext.News.OrderByDescending(x=>x.DateUp).Skip(0).Take(3).ToListAsync();

        homeResponse.Top3CanHo = await _bdsCShapContext.Products.OrderByDescending(x=>x.ProductId)
            .Where(x=>x.CategoryId==(Int32)CategoryProduct.CanHo).Skip(0).Take(6).ToListAsync();
        homeResponse.Top3DatNen = await _bdsCShapContext.Products.OrderByDescending(x => x.ProductId)
            .Where(x => x.CategoryId==(Int32)CategoryProduct.DatNen).Skip(0).Take(6).ToListAsync();
        homeResponse.Top3NhaPho = await _bdsCShapContext.Products.OrderByDescending(x => x.ProductId)
            .Where(x => x.CategoryId==(Int32)CategoryProduct.NhaPho).Skip(0).Take(6).ToListAsync();
        homeResponse.Top3BietThu = await _bdsCShapContext.Products.OrderByDescending(x => x.ProductId)
            .Where(x => x.CategoryId==(Int32)CategoryProduct.BietThu).Skip(0).Take(6).ToListAsync();

        return Json(homeResponse);
    }
}

// public class RequiredHome
// {
//     [Required]
//     public string NewPassword { get; set; }
// }
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
    private readonly Bds_CShapContext _context;
    public HomeController(Bds_CShapContext context)
    {
        _context = context;
    }
    
    // #region Controller View
    // #endregion

    /// <summary>
    /// Controller View
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult Index()
    {
        return View("Index");
    }

    /// <summary>
    /// API
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> HomeData()
    {
        var homeResponse = new HomeResponse();
        homeResponse.Top3News = await _context.News.OrderByDescending(x=>x.DateUp).Skip(0).Take(3).ToListAsync();

        homeResponse.Top3CanHo = await _context.Products.OrderByDescending(x=>x.ProductId)
            .Where(x=>x.CategoryId==(Int32)CategoryProduct.CanHo).Skip(0).Take(6).ToListAsync();
        homeResponse.Top3DatNen = await _context.Products.OrderByDescending(x => x.ProductId)
            .Where(x => x.CategoryId==(Int32)CategoryProduct.DatNen).Skip(0).Take(6).ToListAsync();
        homeResponse.Top3NhaPho = await _context.Products.OrderByDescending(x => x.ProductId)
            .Where(x => x.CategoryId==(Int32)CategoryProduct.NhaPho).Skip(0).Take(6).ToListAsync();
        homeResponse.Top3BietThu = await _context.Products.OrderByDescending(x => x.ProductId)
            .Where(x => x.CategoryId==(Int32)CategoryProduct.BietThu).Skip(0).Take(6).ToListAsync();

        return Json(homeResponse);
    }
}

// public class RequiredHome
// {
//     [Required]
//     public string NewPassword { get; set; }
// }
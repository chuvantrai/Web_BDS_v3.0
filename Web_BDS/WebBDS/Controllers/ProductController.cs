using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBDS.Commons;
using WebBDS.Extensions;
using WebBDS.Models;
using WebBDS.ResponseModels;

namespace WebBDS.Controllers;

public class ProductController : Controller
{
    private readonly Bds_CShapContext _context;

    public ProductController(Bds_CShapContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult ProductDetail(int? id)
    {
        if (id is null)
        {
            return View("Error");
        }
        else
        {
            return View("/Views/Public/ProductsList.cshtml");
        }
    }

    [HttpGet]
    public ActionResult ProductList()
    {
        return View("/Views/Public/ProductsList.cshtml");
    }

    [HttpGet]
    public async Task<ActionResult> ProductDetailData(int? id)
    {
        if (id is not null)
        {
            var product = new ProductDetailResponse();
            product.ProductDetail = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product.ProductDetail is not null)
            {
                product.Top4Products =
                    await _context.Products.Where(x => x.ProductId != id).Skip(0).Take(4).ToListAsync();
                return Json(product);
            }

            return Json("NullData");
        }

        return Json("NullData");
    }

    [HttpGet]
    public async Task<ActionResult> ProductListData(int pageIndex, string? keySearch, 
        CategoryProduct? categoryProduct, List<int>? listRegional,
        SortProducts? sortTime, SortProducts? sortPrice, SortProducts? sortArea, SortProducts? sortWidth)
    {
        var pageSize = 3;
        var productListResponse = new ProductListResponse();
        if (pageIndex == 0) pageIndex = 1;
        Expression<Func<Product, bool>> whereExpression = x => true;
        Expression<Func<Product, string?>> sortExpression = x => null;
        Expression<Func<Product, bool>> exprCategory = x => false;
        Expression<Func<Product, bool>> exprRegional = x => false;
        Expression<Func<Product, bool>> exprSearch = x => false;

        if (categoryProduct is not null)
        {
            exprCategory = exprCategory.Or(x => x.CategoryId == (int)categoryProduct);
            whereExpression = whereExpression.And(exprCategory);
        }

        if (listRegional is not null)
        {
            foreach (var regional in listRegional)
            {
                exprRegional = exprRegional.Or(x => x.RegionalId == regional);
                whereExpression = whereExpression.And(exprRegional);
            }
        }

        if (!string.IsNullOrEmpty(keySearch))
        {
            exprSearch = exprSearch.Or(x => x.ProductName.Contains(keySearch) || keySearch.Contains(x.ProductName));
            whereExpression = whereExpression.And(exprSearch);
        }

        var listSort = new String[]{};
        if (sortWidth is not null)
        {
            listSort = listSort.AddToLastArrayStrings(sortWidth == SortProducts.Oldest ? "DateUp" : "-" + "DateUp");
        }
        if (sortArea is not null)
        {
            listSort = listSort.AddToLastArrayStrings(sortArea == SortProducts.SmallArea ? "AreaM2" : "-" + "AreaM2");
        }
        if (sortPrice is not null)
        {
            listSort = listSort.AddToLastArrayStrings(sortPrice == SortProducts.LowPrice ? "NoPrice" : "-" + "NoPrice");
        }
        if (sortTime is not null)
        {
            listSort = listSort.AddToLastArrayStrings(sortTime == SortProducts.Oldest ? "DateUp" : "-" + "DateUp");
        }

        var totalP = await _context.Products.Where(whereExpression).CountAsync();
        // var listSort = new String[]{sortByWidth,sortByArea,sortByPrice,sortByTime};
        
        productListResponse.TotalPage = totalP / pageSize + (totalP % pageSize > 0 ? 1 : 0);

        productListResponse.ProductsList = await _context.Products.Where(whereExpression)
            .SortBy(listSort)
            .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

        return Json(productListResponse);
    }
}
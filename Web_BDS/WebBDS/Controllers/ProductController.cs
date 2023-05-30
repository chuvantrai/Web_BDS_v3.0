using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBDS.Commons;
using WebBDS.Extensions;
using WebBDS.Models;
using WebBDS.RequestModels;
using WebBDS.ResponseModels;
using WebBDS.FilterPermissions;

namespace WebBDS.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class ProductController : Controller
{
    private readonly Bds_CShapContext _context;
    private readonly ExtensionFile _extensionFile;

    public ProductController(Bds_CShapContext context, ExtensionFile extensionFile)
    {
        _context = context;
        _extensionFile = extensionFile;
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
    [FilterPermission(Action = ActionFilterEnum.ProductDetailData,UserRole = new []{UserRoleEnum.Admin,UserRoleEnum.Customer})]
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
    public async Task<ActionResult> ProductListData2(int pageIndex, string? keySearch, 
        CategoryProduct? categoryProduct, List<int>? listRegional,
        SortProducts? sort)
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

        var bySort = "";

        switch (sort)
        {
            case SortProducts.Latest:
                bySort = "-DateUp";
                break;
            case SortProducts.Oldest:
                bySort = "DateUp";
                break;
            case SortProducts.HighPrice:
                bySort = "-AreaM2";
                break;
            case SortProducts.LowPrice:
                bySort = "AreaM2";
                break;
            case SortProducts.LargeArea:
                bySort = "-NoPrice";
                break;
            case SortProducts.SmallArea:
                bySort = "NoPrice";
                break;
            case SortProducts.LargeWidth:
                bySort = "-HorizontalM";
                break;
            case SortProducts.SmallWidth:
                bySort = "HorizontalM";
                break;
        }

        var totalP = await _context.Products
            .Where(whereExpression)
            .CountAsync();
        
        productListResponse.TotalPage = totalP / pageSize + (totalP % pageSize > 0 ? 1 : 0);

        productListResponse.ProductsList = await _context.Products
            .Where(whereExpression)
            .SortBy(bySort)
            .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

        return Json(productListResponse);
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
            listSort = listSort.AddToLastArrayStrings(sortWidth == SortProducts.Oldest ? "HorizontalM" : "-" + "HorizontalM");
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
    
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromQuery] CreateProductRequest createProductRequest)
    {
        var product = new Product()
        {
            ProductName = createProductRequest.ProductName,
            Description = createProductRequest.Description,
            CategoryId = createProductRequest.CategoryId,
            RegionalId = createProductRequest.RegionalId,
            LetterPrice = createProductRequest.LetterPrice,
            NoPrice = createProductRequest.NoPrice,
            DateUp = DateTime.Now,
            LinkGgmap = createProductRequest.LinkGgmap,
            AreaM2 = createProductRequest.AreaM2,
            HorizontalM = createProductRequest.HorizontalM,
            Status = createProductRequest.Status
        };
        if (createProductRequest.ImgAvar is not null)
        {
            product.ImgAvar = await _extensionFile.CreateImage(createProductRequest.ImgAvar);
        }
        else
        {
            return BadRequest("quá trình up ảnh lỗi!");
        }
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        var productResult = JsonConvert.SerializeObject(product);

        if (createProductRequest.ListImgOther is not null)
        {
            try
            {
                foreach (var imgFile in createProductRequest.ListImgOther)
                {
                    await _context.ImageProducts.AddAsync(new ImageProduct()
                    {
                        ProductId = product.ProductId,
                        ImgName = await _extensionFile.CreateImage(imgFile)
                    });
                }
            }
            catch 
            {
                return BadRequest("quá trình up ảnh lỗi!");
            }
        }
        await _context.SaveChangesAsync();
        return Ok(productResult);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromQuery] UpdateProductRequest updateProductRequest)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x=>x.ProductId==updateProductRequest.ProductId);
        if (product is null)
        {
            return BadRequest("không tìm thấy product cần update"); 
        }
        product.ProductId = updateProductRequest.ProductId;
        product.ProductName = updateProductRequest.ProductName;
        product.Description = updateProductRequest.Description;
        product.CategoryId = updateProductRequest.CategoryId;
        product.RegionalId = updateProductRequest.RegionalId;
        product.LetterPrice = updateProductRequest.LetterPrice;
        product.NoPrice = updateProductRequest.NoPrice;
        product.LinkGgmap = updateProductRequest.LinkGgmap;
        product.AreaM2 = updateProductRequest.AreaM2;
        product.HorizontalM = updateProductRequest.HorizontalM;
        product.Status = updateProductRequest.Status;
        if (updateProductRequest.ImgAvar is not null)
        {
            product.ImgAvar = await _extensionFile.CreateImage(updateProductRequest.ImgAvar);
        }
        else
        {
            return BadRequest("quá trình up ảnh lỗi!");
        }
        await _context.SaveChangesAsync();
        return Ok(product);
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteProduct(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product is null)
        {
            return BadRequest("không tìm thấy product cần delete"); 
        }
        else
        {
            _context.Products.Remove(product);
        }
        await _context.SaveChangesAsync();
        return Ok();
    }
}
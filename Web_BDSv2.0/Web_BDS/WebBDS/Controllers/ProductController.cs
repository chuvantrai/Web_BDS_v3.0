using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBDS.Application.RequestModels;
using WebBDS.Application.ResponseModels;
using WebBDS.Emuns;
using WebBDS.Extensions;
using WebBDS.FilterPermissions;
using WebBDS.Models;

namespace WebBDS.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly Bds_CShapContext _context;
    private readonly ExtensionFile _extensionFile;

    public ProductController(Bds_CShapContext context, ExtensionFile extensionFile)
    {
        _context = context;
        _extensionFile = extensionFile;
    }

    [HttpGet]
    [FilterPermission(Action = ActionFilterEnum.ProductDetailData)]
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
                return Ok(product);
            }

            return Ok("NullData");
        }

        return Ok("NullData");
    }

    [HttpGet]
    public async Task<ActionResult> ProductListData2([FromQuery] ProductListDataRequest request)
    {
        var pageSize = 3;
        var productListResponse = new ProductListResponse();
        if (request.pageIndex == 0) request.pageIndex = 1;
        Expression<Func<Product, bool>> whereExpression = x => true;
        Expression<Func<Product, string?>> sortExpression = x => null;
        Expression<Func<Product, bool>> exprCategory = x => false;
        Expression<Func<Product, bool>> exprRegional = x => false;
        Expression<Func<Product, bool>> exprSearch = x => false;

        if (request.categoryProduct is not null)
        {
            exprCategory = exprCategory.Or(x => x.CategoryId == (int)request.categoryProduct);
            whereExpression = whereExpression.And(exprCategory);
        }

        if (request.listRegional is not null)
        {
            foreach (var regional in request.listRegional)
            {
                exprRegional = exprRegional.Or(x => x.RegionalId == regional);
                whereExpression = whereExpression.And(exprRegional);
            }
        }

        if (!string.IsNullOrEmpty(request.keySearch))
        {
            exprSearch = exprSearch.Or(x =>
                x.ProductName.Contains(request.keySearch) || request.keySearch.Contains(x.ProductName));
            whereExpression = whereExpression.And(exprSearch);
        }

        var bySort = "";

        switch (request.sort)
        {
            case SortProductsEnum.Latest:
                bySort = "-DateUp";
                break;
            case SortProductsEnum.Oldest:
                bySort = "DateUp";
                break;
            case SortProductsEnum.HighPrice:
                bySort = "-AreaM2";
                break;
            case SortProductsEnum.LowPrice:
                bySort = "AreaM2";
                break;
            case SortProductsEnum.LargeArea:
                bySort = "-NoPrice";
                break;
            case SortProductsEnum.SmallArea:
                bySort = "NoPrice";
                break;
            case SortProductsEnum.LargeWidth:
                bySort = "-HorizontalM";
                break;
            case SortProductsEnum.SmallWidth:
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
            .Skip(pageSize * (request.pageIndex - 1)).Take(pageSize).ToListAsync();

        return Ok(productListResponse);
    }

    [HttpGet]
    public async Task<ActionResult> ProductListData([FromQuery] ProductListDataRequest request)
    {
        var pageSize = 3;
        var productListResponse = new ProductListResponse();
        if (request.pageIndex == 0) request.pageIndex = 1;
        Expression<Func<Product, bool>> whereExpression = x => true;
        Expression<Func<Product, string?>> sortExpression = x => null;
        Expression<Func<Product, bool>> exprCategory = x => false;
        Expression<Func<Product, bool>> exprRegional = x => false;
        Expression<Func<Product, bool>> exprSearch = x => false;

        if (request.categoryProduct is not null)
        {
            exprCategory = exprCategory.Or(x => x.CategoryId == (int)request.categoryProduct);
            whereExpression = whereExpression.And(exprCategory);
        }

        if (request.listRegional is not null)
        {
            foreach (var regional in request.listRegional)
            {
                exprRegional = exprRegional.Or(x => x.RegionalId == regional);
                whereExpression = whereExpression.And(exprRegional);
            }
        }

        if (!string.IsNullOrEmpty(request.keySearch))
        {
            exprSearch = exprSearch.Or(x =>
                x.ProductName.Contains(request.keySearch) || request.keySearch.Contains(x.ProductName));
            whereExpression = whereExpression.And(exprSearch);
        }

        var listSort = new String[] { };
        if (request.sortWidth is not null)
        {
            listSort = listSort.AddToLastArrayStrings(request.sortWidth == SortProductsEnum.Oldest
                ? "HorizontalM"
                : "-" + "HorizontalM");
        }

        if (request.sortArea is not null)
        {
            listSort = listSort.AddToLastArrayStrings(request.sortArea == SortProductsEnum.SmallArea
                ? "AreaM2"
                : "-" + "AreaM2");
        }

        if (request.sortPrice is not null)
        {
            listSort = listSort.AddToLastArrayStrings(request.sortPrice == SortProductsEnum.LowPrice
                ? "NoPrice"
                : "-" + "NoPrice");
        }

        if (request.sortTime is not null)
        {
            listSort = listSort.AddToLastArrayStrings(request.sortTime == SortProductsEnum.Oldest
                ? "DateUp"
                : "-" + "DateUp");
        }

        var totalP = await _context.Products.Where(whereExpression).CountAsync();
        // var listSort = new String[]{sortByWidth,sortByArea,sortByPrice,sortByTime};

        productListResponse.TotalPage = totalP / pageSize + (totalP % pageSize > 0 ? 1 : 0);

        productListResponse.ProductsList = await _context.Products.Where(whereExpression)
            .SortBy(listSort)
            .Skip(pageSize * (request.pageIndex - 1)).Take(pageSize).ToListAsync();

        return Ok(productListResponse);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromForm] CreateProductRequest createProductRequest)
    {
        var product = new Product
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
        // var productResult = JsonConvert.SerializeObject(product);

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
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductRequest updateProductRequest)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == updateProductRequest.ProductId);
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
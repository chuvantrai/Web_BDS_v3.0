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
    private readonly IExtensionFile _extensionFile;
    private readonly IMapObject _MapObject;

    public ProductController(Bds_CShapContext context, IExtensionFile extensionFile, IMapObject mapObject)
    {
        _context = context;
        _extensionFile = extensionFile;
        _MapObject = mapObject;
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
    public async Task<ActionResult> ProductListData([FromQuery] ProductListDataRequest request)
    {
        try
        {
            var pageSize = request.PageSize == 0 ? 3 : request.PageSize;
            var productListResponse = new ProductListResponse();
            if (request.PageIndex == 0) request.PageIndex = 1;
            Expression<Func<Product, bool>> whereExpression = x => true;
            Expression<Func<Product, string?>> sortExpression = x => null;
            Expression<Func<Product, bool>> exprCategory = x => false;
            Expression<Func<Product, bool>> exprRegional = x => false;
            Expression<Func<Product, bool>> exprSearch = x => false;

            if (request.ListCategory is not null)
            {
                foreach (var category in request.ListCategory)
                {
                    exprCategory = exprCategory.Or(x => x.CategoryId == category);
                }
                whereExpression = whereExpression.And(exprCategory);
            }

            if (request.Regional is not null)
            {
                exprRegional = exprRegional.Or(x => x.RegionalId == request.Regional);
                whereExpression = whereExpression.And(exprRegional);
            }

            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                exprSearch = exprSearch.Or(x =>
                    x.ProductName.Contains(request.KeySearch) || request.KeySearch.Contains(x.ProductName));
                whereExpression = whereExpression.And(exprSearch);
            }

            var bySort = "-DateUp";

            switch (request.Sort)
            {
                case SortProductEnum.NewProduct:
                    bySort = "-DateUp";
                    break;
                case SortProductEnum.PriceHigh:
                    bySort = "NoPrice";
                    break;
                case SortProductEnum.PriceLow:
                    bySort = "-NoPrice";
                    break;
                case SortProductEnum.PriceMeterLow:
                    bySort = "PriceM2";
                    break;
                case SortProductEnum.PriceMeterHigh:
                    bySort = "-PriceM2";
                    break;
                case SortProductEnum.AcreageHigh:
                    bySort = "AreaM2";
                    break;
                case SortProductEnum.AcreageLow:
                    bySort = "-AreaM2";
                    break;
            }

            var productsList = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Regional)
                .Include(x => x.ImageProducts)
                .Where(whereExpression)
                .Select(x => _MapObject.MapProductResponse(x))
                .ToListAsync();

            var totalP = productsList.Count;
            productListResponse.TotalPage = totalP / pageSize + (totalP % pageSize > 0 ? 1 : 0);
            productListResponse.ProductsList = productsList
                .SortBy(bySort)
                .Skip(pageSize * (request.PageIndex - 1))
                .Take(pageSize).ToList();

            return Ok(productListResponse.ProductsList);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromForm] CreateProductRequest createProductRequest)
    {
        try
        {
            var product = new Product
            {
                ProductName = createProductRequest.ProductName,
                Description = createProductRequest.Description,
                CategoryId = createProductRequest.CategoryId,
                RegionalId = createProductRequest.RegionalId,
                NoPrice = createProductRequest.NoPrice,
                LetterPrice = ExpressionLogic
                    .ConvertPriceToString(createProductRequest.NoPrice),
                DateUp = DateTime.Now,
                LinkGgmap = createProductRequest.LinkGgmap,
                AreaM2 = createProductRequest.AreaM2,
                HorizontalM = createProductRequest.HorizontalM,
                Status = true,
                ImgAvar = createProductRequest.ImgAvatar
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            // var productResult = JsonConvert.SerializeObject(product);

            if (createProductRequest.ListImgOther != null)
            {
                foreach (var imgFile in createProductRequest.ListImgOther)
                {
                    await _context.ImageProducts.AddAsync(new ImageProduct
                    {
                        ProductId = product.ProductId,
                        ImgName = imgFile
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
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
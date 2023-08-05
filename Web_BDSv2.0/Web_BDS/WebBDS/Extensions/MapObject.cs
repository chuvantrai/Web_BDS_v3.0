using WebBDS.Application.ResponseModels;
using WebBDS.Models;

namespace WebBDS.Extensions;

public class MapObject : IMapObject
{
    public ProductResponse MapProductResponse(Product product)
    {
        return new ProductResponse
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Description = product.Description,
            CategoryId = product.CategoryId,
            RegionalId = product.RegionalId,
            LetterPrice = product.LetterPrice,
            NoPrice = product.NoPrice,
            DateUp = product.DateUp,
            LinkGgmap = product.LinkGgmap,
            AreaM2 = product.AreaM2,
            HorizontalM = product.HorizontalM,
            Status = product.Status,
            ImgAvar = product.ImgAvar,
            Category = MapCategoryResponse(product.Category),
            Regional = MapRegionalResponse(product.Regional),
            ImageProducts = product.ImageProducts
                .Select(MapImageProductResponse)
                .ToList(),
            PriceM2 = Math.Round((double)(product.NoPrice / product.AreaM2!), 1)
        };
    }

    public CategoryResponse MapCategoryResponse(Category category)
    {
        return new CategoryResponse()
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName
        };
    }

    public RegionalResponse MapRegionalResponse(Regional regional)
    {
        return new RegionalResponse()
        {
            RegionalId = regional.RegionalId,
            RegionalName = regional.RegionalName
        };
    }

    public ImageProductResponse MapImageProductResponse(ImageProduct img)
    {
        return new ImageProductResponse()
        {
            ImgId = img.ImgId,
            ProductId = img.ProductId,
            ImgName = img.ImgName
        };
    }
}
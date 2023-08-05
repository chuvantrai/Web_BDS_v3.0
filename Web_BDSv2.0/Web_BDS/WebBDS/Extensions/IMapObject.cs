using WebBDS.Application.ResponseModels;
using WebBDS.Models;

namespace WebBDS.Extensions;

public interface IMapObject
{
    public ProductResponse MapProductResponse(Product product);
    public CategoryResponse MapCategoryResponse(Category category);
    public RegionalResponse MapRegionalResponse(Regional regional);
    public ImageProductResponse MapImageProductResponse(ImageProduct img);
}
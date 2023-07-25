using WebBDS.Models;

namespace WebBDS.Application.ResponseModels;

public class ProductDetailResponse
{
    public List<Product>? Top4Products { get; set; }
    public Product? ProductDetail { get; set; }
}
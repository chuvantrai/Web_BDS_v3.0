using WebBDS.Models;

namespace WebBDS.ResponseModels;

public class ProductDetailResponse
{
    public List<Product>? Top4Products { get; set; }
    public Product? ProductDetail { get; set; }
}
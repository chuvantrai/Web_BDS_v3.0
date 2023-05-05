using WebBDS.Models;

namespace WebBDS.ResponseModels;

public class ProductListResponse
{
    public List<Product>? ProductsList { get; set; }
    public int TotalPage { get; set; }
}
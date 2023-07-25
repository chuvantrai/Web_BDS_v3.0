using WebBDS.Models;

namespace WebBDS.Application.ResponseModels;

public class ProductListResponse
{
    public List<Product>? ProductsList { get; set; }
    public int TotalPage { get; set; }
}
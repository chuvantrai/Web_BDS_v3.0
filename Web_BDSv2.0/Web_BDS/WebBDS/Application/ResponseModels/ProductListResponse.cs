using WebBDS.Models;

namespace WebBDS.Application.ResponseModels;

public class ProductListResponse
{
    public List<ProductResponse>? ProductsList { get; set; }
    public int TotalPage { get; set; }
}
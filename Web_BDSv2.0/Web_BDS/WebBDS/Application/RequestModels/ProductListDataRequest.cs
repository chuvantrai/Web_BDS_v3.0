using WebBDS.Emuns;

namespace WebBDS.Application.RequestModels;

public class ProductListDataRequest
{
    public int pageIndex { get; set; }
    public string? keySearch { get; set; }
    public CategoryProductEnum? categoryProduct { get; set; }
    public List<int>? listRegional { get; set; }
    public SortProductsEnum? sortTime { get; set; }
    public SortProductsEnum? sortPrice { get; set; }
    public SortProductsEnum? sortArea { get; set; }
    public SortProductsEnum? sortWidth { get; set; }
    public SortProductsEnum? sort { get; set; }
}
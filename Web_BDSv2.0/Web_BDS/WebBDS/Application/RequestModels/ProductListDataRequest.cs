using WebBDS.Emuns;

namespace WebBDS.Application.RequestModels;

public class ProductListDataRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? KeySearch { get; set; }
    public List<int>? ListCategory { get; set; }
    public int? Regional { get; set; }
    public SortProductEnum? Sort { get; set; }
}
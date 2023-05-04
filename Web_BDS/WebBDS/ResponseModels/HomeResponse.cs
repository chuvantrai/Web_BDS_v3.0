using WebBDS.Models;

namespace WebBDS.ResponseModels;

public class HomeResponse
{
    public List<News> Top3News { get; set; } = default!;
    public List<Product> Top3CanHo { get; set; } = default!;
    public List<Product> Top3DatNen { get; set; } = default!;
    public List<Product> Top3NhaPho { get; set; } = default!;
    public List<Product> Top3BietThu { get; set; } = default!;
}
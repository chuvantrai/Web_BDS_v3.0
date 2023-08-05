using WebBDS.Models;

namespace WebBDS.Application.ResponseModels;

public class ProductResponse
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public int RegionalId { get; set; }
    public string LetterPrice { get; set; } = null!;
    public long NoPrice { get; set; }
    public DateTime DateUp { get; set; }
    public string? LinkGgmap { get; set; }
    public double? AreaM2 { get; set; }
    public double? HorizontalM { get; set; }
    public bool Status { get; set; }
    public string? ImgAvar { get; set; }

    public CategoryResponse Category { get; set; } = null!;
    public RegionalResponse Regional { get; set; } = null!;
    public List<ImageProductResponse> ImageProducts { get; set; }
    
    public double? PriceM2 { get; set; }
}
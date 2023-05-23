namespace WebBDS.RequestModels;

public class CreateProductRequest
{
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public int RegionalId { get; set; }
    public string LetterPrice { get; set; } = null!;
    public long NoPrice { get; set; }
    public string? LinkGgmap { get; set; }
    public double? AreaM2 { get; set; }
    public double? HorizontalM { get; set; }
    public bool Status { get; set; }
    public IFormFile? ImgAvar { get; set; }
}
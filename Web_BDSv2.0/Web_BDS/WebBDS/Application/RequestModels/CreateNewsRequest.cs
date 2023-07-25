namespace WebBDS.Application.RequestModels;

public class CreateNewsRequest
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? ImgAvar { get; set; }
}
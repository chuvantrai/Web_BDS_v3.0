namespace WebBDS.RequestModels;

public class UpdateNewsRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? ImgAvar { get; set; }
    public DateTime DateUp { get; set; }
}
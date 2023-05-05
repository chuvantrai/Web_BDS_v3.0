using WebBDS.Models;

namespace WebBDS.ResponseModels;

public class NewsListResponse
{
    public List<News>? NewsList { get; set; }
    public int TotalPage { get; set; }
}
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBDS.Application.RequestModels;
using WebBDS.Application.ResponseModels;
using WebBDS.Emuns;
using WebBDS.Extensions;
using WebBDS.Models;

namespace WebBDS.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly Bds_CShapContext _context;
    private readonly IExtensionFile _extensionFile;

    public NewsController(Bds_CShapContext context, IExtensionFile extensionFile)
    {
        _context = context;
        _extensionFile = extensionFile;
    }
    
    [HttpGet]
    public async Task<ActionResult> NewsDetailData(int? id)
    {
        if (id is not null)
        {
            var news = new NewsDetailResponse();
            news.NewsDetail = await _context.News.FirstOrDefaultAsync(x => x.NewsId == id);
            if (news.NewsDetail is not null)
            {
                news.Top3News = await _context.News.Where(x => x.NewsId != id).Skip(0).Take(4).ToListAsync();
                return Ok(news);
            }
        }

        return NotFound("NullData");
    }

    [HttpGet]
    public async Task<ActionResult> NewsListData(int pageIndex, string? keySearch, SortNewsEnum? sort)
    {
        var pageSize = 3;
        var newsListResponse = new NewsListResponse();
        if (pageIndex == 0) pageIndex = 1;
        Expression<Func<News, bool>> whereExpression = x => true;
        Expression<Func<News, string?>> sortExpression = x => null;
        Expression<Func<News, bool>> expr1 = x => false;

        if (!string.IsNullOrEmpty(keySearch))
        {
            expr1 = expr1.Or(x => keySearch.Contains(x.Title) || x.Title.Contains(keySearch));
            whereExpression = whereExpression.And(expr1);
        }

        var newsList = new List<News>();
        if (sort is not null)
        {
            switch (sort)
            {
                case SortNewsEnum.Latest:
                    newsList = await _context.News.Where(whereExpression).OrderByDescending(x => x.DateUp)
                        .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
                    break;
                case SortNewsEnum.Oldest:
                    newsList = await _context.News.Where(whereExpression).OrderBy(x => x.DateUp)
                        .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
                    break;
            }
        }
        else
        {
            newsList = await _context.News.Where(whereExpression).Skip(pageSize * (pageIndex - 1)).Take(pageSize)
                .ToListAsync();
        }

        var totalP = await _context.News.Where(whereExpression).CountAsync();
        newsListResponse.TotalPage = totalP / pageSize + (totalP % pageSize > 0 ? 1 : 0);
        newsListResponse.NewsList = newsList;
        return Ok(newsListResponse);
    }

    [HttpPost]
    public async Task<ActionResult> CreateNews([FromQuery] CreateNewsRequest createNewsRequest)
    {
        var news = new News()
        {
            Title = createNewsRequest.Title,
            Content = createNewsRequest.Content,
            DateUp = DateTime.Now
        };
        if (createNewsRequest.ImgAvar is not null)
        {
            news.ImgAvar = await _extensionFile.CreateImage(createNewsRequest.ImgAvar);
        }
        else
        {
            // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Thông báo lỗi"); 
            return BadRequest("quá trình up ảnh lỗi!");
        }

        await _context.News.AddAsync(news);
        await _context.SaveChangesAsync();
        return Ok(news);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateNews([FromQuery] UpdateNewsRequest updateNewsRequest)
    {
        var news = await _context.News.FirstOrDefaultAsync(x => x.NewsId == updateNewsRequest.Id);

        if (news is null)
        {
            return BadRequest("không tìm thấy news cần update");
        }

        news.Title = updateNewsRequest.Title;
        news.Content = updateNewsRequest.Content;
        news.DateUp = updateNewsRequest.DateUp;
        if (updateNewsRequest.ImgAvar is not null)
        {
            news.ImgAvar = await _extensionFile.CreateImage(updateNewsRequest.ImgAvar);
        }

        await _context.SaveChangesAsync();
        return Ok(news);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteNews(int newsId)
    {
        var news = await _context.News.FirstOrDefaultAsync(x => x.NewsId == newsId);

        if (news is null)
        {
            return BadRequest("không tìm thấy news cần delete");
        }
        else
        {
            _context.News.Remove(news);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}
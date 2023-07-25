using System.Linq.Expressions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBDS.Commons;
using WebBDS.Extensions;
using WebBDS.Models;
using WebBDS.RequestModels;
using WebBDS.ResponseModels;

namespace WebBDS.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class NewsController : Controller
{
    private readonly Bds_CShapContext _context;
    private readonly ExtensionFile _extensionFile;

    public NewsController(Bds_CShapContext context, ExtensionFile extensionFile)
    {
        _context = context;
        _extensionFile = extensionFile;
    }

    /// <summary>
    /// Controller View
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult NewsDetail(int? id)
    {
        if (id is null)
        {
            return View("Error");
        }
        else
        {
            return View("/Views/Public/NewsDetail.cshtml");
        }
    }

    [HttpGet]
    public ActionResult NewsList()
    {
        return View("/Views/Public/NewsList.cshtml");
    }
    
    /// <summary>
    /// API
    /// </summary>
    /// <returns></returns>
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
                return Json(news);
            }

            return Json("NullData");
        }

        return Json("NullData");
    }

    [HttpGet]
    public async Task<ActionResult> NewsListData(int pageIndex, string? keySearch, SortNews? sort)
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
                case SortNews.Latest:
                    newsList = await _context.News.Where(whereExpression).OrderByDescending(x => x.DateUp)
                        .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
                    break;
                case SortNews.Oldest:
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
        return Json(newsListResponse);
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
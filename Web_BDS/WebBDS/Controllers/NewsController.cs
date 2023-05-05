using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBDS.Commons;
using WebBDS.Extensions;
using WebBDS.Models;
using WebBDS.ResponseModels;

namespace WebBDS.Controllers;

public class NewsController : Controller
{
    private readonly Bds_CShapContext _context;
    public NewsController(Bds_CShapContext context)
    {
        _context = context;
    }
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

    [HttpGet]
    public async Task<ActionResult> NewsDetailData(int? id)
    {
        if (id is not null)
        {
            var news = new NewsDetailResponse(); 
            news.NewsDetail = await _context.News.FirstOrDefaultAsync(x => x.NewsId == id);
            if (news.NewsDetail is not null)
            {
                news.Top3News = await _context.News.Where(x=>x.NewsId!=id).Skip(0).Take(4).ToListAsync();
                return Json(news);
            }
            return Json("NullData");
        }
        return Json("NullData");
    }
    [HttpGet]
    public async Task<ActionResult> NewsListData(int pageIndex,string? keySearch,SortNews? sort)
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
                    newsList = await _context.News.Where(whereExpression).OrderByDescending(x=>x.DateUp).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
                    break;
                case SortNews.Oldest:
                    newsList = await _context.News.Where(whereExpression).OrderBy(x=>x.DateUp).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
                    break; 
            }   
        }
        else
        {
            newsList = await _context.News.Where(whereExpression).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
        }

        var totalP = await _context.News.Where(whereExpression).CountAsync();
        newsListResponse.TotalPage = totalP / pageSize + (totalP % pageSize > 0 ? 1 : 0);
        newsListResponse.NewsList = newsList;
        return Json(newsListResponse);
    }
}
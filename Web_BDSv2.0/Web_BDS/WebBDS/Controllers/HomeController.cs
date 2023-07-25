using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBDS.Application.ResponseModels;
using WebBDS.Emuns;
using WebBDS.Models;

namespace WebBDS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly Bds_CShapContext _context;

        public HomeController(Bds_CShapContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> HomeData()
        {
            var listAll = await _context.Products.ToListAsync();
            var homeResponse = new HomeResponse();
            if (listAll.Count > 0)
            {
                homeResponse.Top3CanHo = listAll.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (Int32)CategoryProductEnum.CanHo).Skip(0).Take(6).ToList();
                homeResponse.Top3DatNen = listAll.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (Int32)CategoryProductEnum.DatNen).Skip(0).Take(6).ToList();
                homeResponse.Top3NhaPho = listAll.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (Int32)CategoryProductEnum.NhaPho).Skip(0).Take(6).ToList();
                homeResponse.Top3BietThu = listAll.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (Int32)CategoryProductEnum.BietThu).Skip(0).Take(6).ToList();
            }

            homeResponse.Top3News = await _context.News.OrderByDescending(x => x.DateUp).Skip(0).Take(3).ToListAsync();
            return Ok(homeResponse);
        }
    }
}
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebBDS.Application.ResponseModels;
using WebBDS.Emuns;
using WebBDS.Models;

namespace WebBDS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Bds_CShapContext _context;

        public IndexModel(Bds_CShapContext context)
        {
            _context = context;
        }

        public List<News> Top3News { get; set; }
        public List<Product> Top3CanHo { get; set; }
        public List<Product> Top3DatNen { get; set; }
        public List<Product> Top3NhaPho { get; set; }
        public List<Product> Top3BietThu { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                Top3CanHo = await _context.Products.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (int)CategoryProductEnum.CanHo).Skip(0).Take(6).ToListAsync();
                Top3DatNen = await _context.Products.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (int)CategoryProductEnum.DatNen).Skip(0).Take(6).ToListAsync();
                Top3NhaPho = await _context.Products.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (int)CategoryProductEnum.NhaPho).Skip(0).Take(6).ToListAsync();
                Top3BietThu = await _context.Products.OrderByDescending(x => x.ProductId)
                    .Where(x => x.CategoryId == (int)CategoryProductEnum.BietThu).Skip(0).Take(6).ToListAsync();
                Top3News = await _context.News.OrderByDescending(x => x.DateUp).Skip(0).Take(3).ToListAsync();
                return Page();
            }
            catch
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
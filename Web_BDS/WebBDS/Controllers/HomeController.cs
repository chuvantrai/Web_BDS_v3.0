using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using WebBDS.Models;
using System.Linq;

namespace WebBDS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Bds_CShapContext _context;

        public HomeController(ILogger<HomeController> logger, Bds_CShapContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string tb)
        {
            // lay Acc user
            var jsonStr = Request.Cookies["useraccount"];
            User user;
            if (jsonStr is null) { user = new User(); }
            else { user = JsonConvert.DeserializeObject<User>(jsonStr); ViewBag.User = user; }

            using (var context = new Bds_CShapContext())
            {
                var t = context.Users.ToList();
            }

            if (tb!=null&&tb.Equals("1")) { ViewBag.thongbao = "Đã gửi yêu cầu thành công!"; }
            List<News> listnews = _context.News.OrderByDescending(x=>x.DateUp).Skip(0).Take(3).ToList();
            ViewBag.listnews= listnews;

            List<Product> listcanho = _context.Products.OrderByDescending(x=>x.ProductId)
                .Where(x=>x.Category.CategoryName.Equals("Căn hộ")).Skip(0).Take(3).ToList();
            ViewBag.listcanho = listcanho;
            List<Product> listdatnen = _context.Products.OrderByDescending(x => x.ProductId)
                .Where(x => x.Category.CategoryName.Equals("Đất nền")).Skip(0).Take(3).ToList();
            ViewBag.listdatnen = listdatnen;
            List<Product> listnhapho = _context.Products.OrderByDescending(x => x.ProductId)
                .Where(x => x.Category.CategoryName.Equals("Nhà Phố")).Skip(0).Take(3).ToList();
            ViewBag.listnhapho = listnhapho;
            List<Product> listbietthu = _context.Products.OrderByDescending(x => x.ProductId)
                .Where(x => x.Category.CategoryName.Equals("Biệt Thự")).Skip(0).Take(3).ToList();
            ViewBag.listbietthu = listbietthu;
            ViewBag.listregi = _context.Regionals.ToList();
            return View("Index.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebBDS.Application.ResponseModels;
using WebBDS.Models;

namespace WebBDS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client = null;
        private string _serviceUrl { get; set; }
        public IndexModel(HttpClient client)
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            _serviceUrl = "http://localhost:5000/";
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
                HttpResponseMessage response = await _client.GetAsync(_serviceUrl+"api/home/HomeData");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var option = new JsonSerializerOptions
                        { PropertyNameCaseInsensitive = true };
                    var listData = JsonSerializer.Deserialize<HomeResponse>(responseBody, option);
                    Top3News = listData!.Top3News;
                    Top3CanHo = listData.Top3CanHo;
                    Top3DatNen = listData.Top3DatNen;
                    Top3NhaPho = listData.Top3NhaPho;
                    Top3BietThu = listData.Top3BietThu;
                    return Page();
                }
            }
            catch
            {
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/Error");
        }
    }
}
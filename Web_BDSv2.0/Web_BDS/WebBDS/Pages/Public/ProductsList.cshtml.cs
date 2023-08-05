using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebBDS.Application.ResponseModels;

namespace WebBDS.Pages.Public;

public class ProductsList : PageModel
{
    private readonly HttpClient _client = null;
    private string _serviceUrl { get; set; }
    public ProductsList(HttpClient client)
    {
        _client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        _serviceUrl = "http://localhost:5000/";
    }
    public async Task<IActionResult> OnGet()
    {
        return Page();
        // try
        // {
        //     HttpResponseMessage response = await _client.GetAsync(_serviceUrl+"api/home/HomeData");
        //     if (response.IsSuccessStatusCode)
        //     {
        //         string responseBody = await response.Content.ReadAsStringAsync();
        //         var option = new JsonSerializerOptions()
        //             { PropertyNameCaseInsensitive = true };
        //         var listData = JsonSerializer.Deserialize<HomeResponse>(responseBody, option);
        //         return Page();
        //     }
        // }
        // catch
        // {
        //     return RedirectToPage("/Error");
        // }
        // return RedirectToPage("/Error");
    }
}
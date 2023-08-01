using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebBDS.Pages.Public;

public class NewsDetail : PageModel
{
    public IActionResult OnGet(int? id)
    {
        if (id is null)
        {
            return Page();
        }
        else
        {
            return RedirectToPage("/Error");
        }
    }
}
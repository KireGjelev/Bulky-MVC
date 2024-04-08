using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BulkyWeb.Pages.Customer.Home
{
    public class MapModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
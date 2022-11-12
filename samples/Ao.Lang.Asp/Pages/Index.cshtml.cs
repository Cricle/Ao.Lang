using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Ao.Lang.Asp.Pages
{
    public class IndexModel : PageModel
    {
        [FromQuery(Name ="count")]
        [Range(0, 100, ErrorMessage = "hello")]
        public int Count { get; set; }

        public void OnGet()
        {
        }
    }
}

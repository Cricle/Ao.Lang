using Microsoft.AspNetCore.Mvc;

namespace Ao.Lang.Asp.Controller
{
    public class IndexController : ControllerBase
    {
        public IActionResult Index()
        {
            return base.RedirectToPage("Index");
        }
    }
}

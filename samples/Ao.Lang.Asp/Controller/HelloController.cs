using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Ao.Lang.Asp.Controller
{
    public class HelloController : ControllerBase
    {
        private readonly ILanguageRoot languageRoot;
        private readonly IStringLocalizer<HelloController> localizer;

        public HelloController(ILanguageRoot languageRoot, IStringLocalizer<HelloController> localizer)
        {
            this.languageRoot = languageRoot;
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Hello()
        {
            return Ok(new
            {
                root= languageRoot["Ao.Lang.Asp.Controller.HelloController:hello"],
                loc=localizer["hello"].Value
            });
        }
    }
}

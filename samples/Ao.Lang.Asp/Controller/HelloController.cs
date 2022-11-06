using Microsoft.AspNetCore.Mvc;

namespace Ao.Lang.Asp.Controller
{
    public class HelloController : ControllerBase
    {
        private readonly ILanguageRoot languageRoot;

        public HelloController(ILanguageRoot languageRoot)
        {
            this.languageRoot = languageRoot;
        }

        [HttpGet]
        public IActionResult Hello()
        {
            return Ok(languageRoot["hello"]);
        }
    }
}

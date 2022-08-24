using Microsoft.AspNetCore.Mvc;

namespace Involys.Poc.Api.Controllers.Common
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace url_shortener.Controllers
{
    public class ShortUrlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

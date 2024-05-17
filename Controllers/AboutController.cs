using Microsoft.AspNetCore.Mvc;

namespace url_shortener.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

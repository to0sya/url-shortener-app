using Microsoft.AspNetCore.Mvc;

namespace url_shortener.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}

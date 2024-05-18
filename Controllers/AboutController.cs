using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using url_shortener.Models;

namespace url_shortener.Controllers
{
    [Route("/about")]
    public class AboutController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "algorithm_description.txt");

            string text = System.IO.File.ReadAllText(filePath);

            if (text == null)
            {
                text = "No description available";
            }

            ViewBag.Text = text;

            return View(new About() { Text = text});
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string text)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "algorithm_description.txt");

            await System.IO.File.WriteAllTextAsync(filePath, text);

            return RedirectToAction("Index");
        }
    }
}

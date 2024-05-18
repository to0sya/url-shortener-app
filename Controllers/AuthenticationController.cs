using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using url_shortener.Contexts;
using url_shortener.Models;
using url_shortener.Repositories;

namespace url_shortener.Controllers
{
    [AllowAnonymous]
    [Route("/login")]
    public class AuthenticationController : Controller
    {
        private readonly IRepository<User, int> _repository;

        public AuthenticationController(IRepository<User, int> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "ShortUrl");
            } else if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "ShortUrl");
            }

            return View();

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Login", "Password")] User user)
        {
            var usersFromDb = await _repository.GetAll();

            var findUser = usersFromDb.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);

            if (findUser != null)
            {

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, findUser.Login),
                    new(ClaimTypes.Role, findUser.Role),
                    new(ClaimTypes.NameIdentifier, findUser.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("ApplicationCookie", principal);
                return RedirectToAction("Index", "ShortUrl");
            }
            ViewBag.Error = "Invalid login or password";
            return View("Index", user);
        }

        [AllowAnonymous]
        [Route("/login/info")]
        public IActionResult GetRole()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(new { role = "Unauthorized"});
            } else if (User.IsInRole("Admin"))
            {
                return Ok(new { id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, role = "Admin" });
            } else if (User.IsInRole("User"))
            {
                return Ok(new { id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, role = "User" });
            } else
            {
                return BadRequest();
            }
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("ApplicationCookie");
            return RedirectToAction("Index");
        }
    }
}

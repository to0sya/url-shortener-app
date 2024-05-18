using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using url_shortener.Models;
using url_shortener.Repositories;
using url_shortener.Utilities;

namespace url_shortener.Controllers
{
    [Route("")]
    public class ShortUrlController : Controller
    {
        private readonly IRepository<ShortUrl, string> _repository;

        public ShortUrlController(IRepository<ShortUrl, string> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        [Route("short-url/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new { success = "true", data = result });
            }
        }

        [HttpPost]
        [Route("short-url")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] ShortUrlBody body)
        {
            if (_repository.GetAll().Result.Any(url => url.OriginalUrl == body.originalUrl))
            {
                return BadRequest(new {success = "false", message = "This URL already exists" });
            }

            string shortedUrl = ShortUrlService.Short(body.originalUrl);

            var shortUrl = new ShortUrl
            {
                OriginalUrl = body.originalUrl,
                ShortenedUrl = shortedUrl,
                CreationDate = DateTime.UtcNow,
                CreatedBy = body.userId
            };

            ShortUrl result = await _repository.Add(shortUrl);

            if (result != null)
            {
                return Ok(new {success = "true", data = result });
            } else
            {
                return BadRequest(new {success = "false" });
            }
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<IActionResult> Get(string shortUrl)
        {
            var result = await _repository.Get(shortUrl);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Redirect(result.OriginalUrl);
            }
        }

        [HttpGet]
        [Route("short-url/{id}")]
        [Authorize]
        public async Task<IActionResult> Info(int id)
        {
            
            var result = await _repository.GetAll();

            var findUrl = result.FirstOrDefault(url => url.Id == id);

            if (findUrl == null)
            {
                return NotFound();
            }
            else
            {
                findUrl.ShortenedUrl = $"{Request.Scheme}://{Request.Host}/{findUrl.ShortenedUrl}";
                return View("Info", findUrl);
            }
        }

        [HttpGet]
        [Route("short-url")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();

            if (result.ToList().Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(result.Select(url =>
                {
                    string shortUrl = url.ShortenedUrl;
                    url.ShortenedUrl = $"{Request.Scheme}://{Request.Host}/{shortUrl}";
                    return url;
                }));
            }
        }
    }
}

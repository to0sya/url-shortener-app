using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using url_shortener.Models;

namespace url_shortener.Repositories
{
    public class ShortUrlRepository : IRepository<ShortUrl, string>
    {
        private readonly DbContext _context;

        public ShortUrlRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<ShortUrl> Add(ShortUrl entity)
        {
            var entityEntry = await _context.Set<ShortUrl>().AddAsync(entity);
            var answer = await _context.SaveChangesAsync();

            return answer != 0 ? entityEntry.Entity : null;
        }

        public async Task<string> Delete(string url)
        {
            var entity = _context.Set<ShortUrl>().Where(u => u.ShortenedUrl.Equals(url)).SingleOrDefault();

            var entityEntry = _context.Set<ShortUrl>().Remove(entity);

            var result = await _context.SaveChangesAsync();

            return result.ToString();
        }

        public async Task<ShortUrl> Get(string url)
        {
            return await _context.Set<ShortUrl>().Where(u => u.ShortenedUrl.Equals(url)).SingleOrDefaultAsync();
        }


        [AllowAnonymous]
        public async Task<IEnumerable<ShortUrl>> GetAll()
        {
            return await _context.Set<ShortUrl>().Include(u => u.Creator).ToListAsync();
        }
    }
}

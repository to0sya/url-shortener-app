using Microsoft.EntityFrameworkCore;
using url_shortener.Models;

namespace url_shortener.Repositories
{
    public class UserRepository : IRepository<User, int>
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        
        public async Task<User> Add(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Set<User>().ToListAsync();
        }
    }
}

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

        
        public Task<User> Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Set<User>().ToListAsync();
        }
    }
}

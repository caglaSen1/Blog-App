using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete
{

    public class EfUserRepository : IUserRepository
    {
        private BlogAppDbContext _context;

        public EfUserRepository(BlogAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            User? user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
                         
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<User> GetByUrl(string url)
        {
            User? user = await _context.Users
            .FirstOrDefaultAsync(u => u.Url == url)
            ?? throw new KeyNotFoundException($"A user with the url {url} was not found.");

            return user;
        }

        public async Task<User?> GetByEmail(string email)
        {
            User? user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            User? user = await _context.Users
            .Include(u => u.Blogs)
            .Include(u => u.Comments)
            .ThenInclude(c => c.Blog) 
            .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            User? user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                         
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<User?> GetByEmailAndUserName(string email, string userName)
        {
            User? user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email || u.UserName == userName);

            if (user != null)
            {
                return user;
            }
            
            return null;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

    }
}
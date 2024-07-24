using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete{

    public class EfUserRepository : IUserRepository
    {
        private BlogAppDbContext _context;

        public EfUserRepository(BlogAppDbContext context){
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            User user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id)
                         ?? throw new KeyNotFoundException($"A user with the ID {id} was not found.");
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email)
                         ?? throw new KeyNotFoundException($"A user with the email {email} was not found.");
            return user;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        
        
    }
}
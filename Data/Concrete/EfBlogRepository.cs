using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete
{
    public class EfBlogRepository : IBlogRepository
    {
        private readonly BlogAppDbContext _context;

        public EfBlogRepository(BlogAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetAll()
        {
            return await _context.Blogs
            .Include(b => b.Tags)
            .ToListAsync();
        }

        public async Task<Blog> GetById(int id)
        {
            Blog blog = await _context.Blogs
            .Include(b => b.Tags)
            .Include(b => b.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(b => b.Id == id)
                         ?? throw new KeyNotFoundException($"A blog with the ID {id} was not found.");
            return blog;
        }

        public async Task<Blog> GetByUrl(string url)
        {
            Blog blog = await _context.Blogs
            .Include(b => b.Tags)
            .Include(b => b.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(b => b.Url == url)
                         ?? throw new KeyNotFoundException($"A blog with the URL {url} was not found.");
            return blog;
        }

        public async Task<List<Blog>> GetBlogsByUserId(int userId)
        {
            return await _context.Blogs
            .Include(b => b.Tags)
            .Where(b => b.UserId == userId)
            .ToListAsync();
        }

        public void Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public void Delete(Blog blog)
        {
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }
    }
}
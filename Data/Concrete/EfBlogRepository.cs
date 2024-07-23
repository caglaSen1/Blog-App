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

        public IQueryable<Blog> GetAll()
        {
            return _context.Blogs;
        }

        public async Task<Blog> GetById(int id)
        {
            Blog blog = await _context.Blogs
            .Include(b => b.Tags)
            .FirstOrDefaultAsync(b => b.Id == id)
                         ?? throw new KeyNotFoundException($"A blog with the ID {id} was not found.");
            return blog;
        }

        public async Task<Blog> GetByUrl(string url)
        {
            Blog blog = await _context.Blogs
            .Include(b => b.Tags)
            .FirstOrDefaultAsync(b => b.Url == url)
                         ?? throw new KeyNotFoundException($"A blog with the URL {url} was not found.");
            return blog;
        }

        public void Add(Blog entity)
        {
            _context.Blogs.Add(entity);
            _context.SaveChanges();
        }

    }
}
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
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

        public async Task<PagedResult<Blog>> GetPagedBlogs(int pageNumber, int pageSize, string tagUrl, string searchString, int? userId)
        {
            var query = _context.Blogs.AsQueryable();

            if (userId != null)
            {
                query = query.Where(b => b.UserId == userId).AsQueryable();
            }

            if (!string.IsNullOrEmpty(tagUrl))
            {
                query = query.Where(b => b.Tags.Any(t => t.Url == tagUrl));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(lowerSearchString) || b.Content.ToLower().Contains(lowerSearchString));
            }

            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Blog>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
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
            .Include(b => b.User)
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

        public async Task<int> GetCommentCount(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);

            return blog.Comments.Count();
        }

        public async Task<int> GetLikeCount(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);

            return blog.LikeCount;
        }

        public async Task<List<Blog>> GetPopularBlogs(int amount)
        {

            if (amount > _context.Blogs.Count())
            {
                amount = _context.Blogs.Count();
            }

            var popularBlogs = await _context.Blogs
                .Include(b => b.Comments)
                .Select(b => new
                {
                    Blog = b,
                    Popularity = b.Comments.Count + b.LikeCount
                })
                .OrderByDescending(b => b.Popularity)
                .Take(amount)
                .Select(b => b.Blog)
                .ToListAsync();

            return popularBlogs;

        }

        public void Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public void Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }

        public void Delete(Blog blog)
        {
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }

        public void LikeBlog(int blogId)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == blogId);
            blog.LikeCount++;
            _context.SaveChanges();
        }

    }
}
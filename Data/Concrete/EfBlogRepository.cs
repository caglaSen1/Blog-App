using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    public class EfBlogRepository : IBlogRepository
    {
        private readonly BlogAppDbContext _context;

        public EfBlogRepository(BlogAppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Blog> GetAll => _context.Blogs;

        public void Add(Blog entity)
        {
            _context.Blogs.Add(entity);
            _context.SaveChanges();
        }

    }
}
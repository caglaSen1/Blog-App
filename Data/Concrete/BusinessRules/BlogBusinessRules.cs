using BlogApp.Data.Abstract.BusinessRules;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.BusinessRules
{
    public class BlogBusinessRules : IBlogBusinessRules
    {

        private readonly BlogAppDbContext _context;

        public BlogBusinessRules(BlogAppDbContext context)
        {
            _context = context;
        }

        public bool AnyBlogExistWithTitle(string title)
        {
            var blog = _context.Blogs.Any(x => x.Title == title);

            return blog;
        }
    }
}
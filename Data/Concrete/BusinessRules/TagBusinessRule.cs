
using BlogApp.Data.Abstract.BusinessRules;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.BusinessRules
{
    public class TagBusinessRules : ITagBusinessRules
    {

        private readonly BlogAppDbContext _context;

        public TagBusinessRules(BlogAppDbContext context)
        {
            _context = context;
        }

        public bool IsTagExistWithName(string name)
        {
            var tag = _context.Tags.Any(x => x.Name == name);

            return tag;
        }
    }
}
using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    public class EfTagRepository : ITagRepository
    {

        private readonly BlogAppDbContext _context;

        public EfTagRepository(BlogAppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> GetAll => _context.Tags;

        public void Add(Tag entity)
        {
            _context.Tags.Add(entity);
            _context.SaveChanges();
        }
    }
}
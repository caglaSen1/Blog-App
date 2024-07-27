using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete
{
    public class EfTagRepository : ITagRepository
    {

        private readonly BlogAppDbContext _context;

        public EfTagRepository(BlogAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAll()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetByName(string name)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name)
                        ?? throw new KeyNotFoundException($"A tag with the name {name} was not found.");
            return tag;
        }

        public async Task<Tag> GetById(int id)
        {
            Tag tag = await _context.Tags.FindAsync(id)
                        ?? throw new KeyNotFoundException($"A tag with the id {id} was not found.");
            return tag;
        }

        public async Task<Tag> GetByUrl(string url)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Url == url)
                        ?? throw new KeyNotFoundException($"A tag with the url {url} was not found.");
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetByIds(List<int> tagIds){
            return await _context.Tags.Where(t => tagIds.Contains(t.Id)).ToListAsync();
        }

        public async Task<List<Tag>> GetPopularTags(int amount)
        {
            if(amount > await _context.Tags.CountAsync()){
                amount = await _context.Tags.CountAsync();
            }
            
            return await _context.Tags.OrderByDescending(t => t.Blogs.Count())
            .Take(amount)
            .ToListAsync();
        }

        public void Add(Tag entity)
        {
            _context.Tags.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }

    }
}
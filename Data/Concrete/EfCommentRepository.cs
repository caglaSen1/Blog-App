using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete{

    public class EfCommentRepository : ICommentRepository
    {

        private readonly BlogAppDbContext _context;

        public EfCommentRepository(BlogAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _context.Comments
            .Include(c => c.User)
            .ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            Comment comment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id)
                         ?? throw new KeyNotFoundException($"A comment with the ID {id} was not found.");
            return comment;
        }

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
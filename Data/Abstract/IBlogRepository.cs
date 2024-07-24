using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAll();

        Task<Blog> GetById(int id);
        
        Task<Blog> GetByUrl(string url);

        void Add(Blog blog);
    }
}
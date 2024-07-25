using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAll();

        Task<Blog> GetById(int id);
        
        Task<Blog> GetByUrl(string url);

        Task<List<Blog>> GetBlogsByUserId(int userId);

        void Add(Blog blog);

        void Update(Blog blog);

        void Delete(Blog blog);
    }
}
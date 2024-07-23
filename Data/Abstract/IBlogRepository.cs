using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IBlogRepository
    {
        IQueryable<Blog> GetAll();

        Task<Blog> GetById(int id);
        
        Task<Blog> GetByUrl(string url);

        void Add(Blog entity);
    }
}
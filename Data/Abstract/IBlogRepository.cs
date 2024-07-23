using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IBlogRepository
    {
        IQueryable<Blog> GetAll { get; }

        void Add(Blog entity);
    }
}
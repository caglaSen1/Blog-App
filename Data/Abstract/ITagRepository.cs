using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> GetAll();

        Task<Tag> GetByName(string name);

        void Add(Tag entity);
    }
}
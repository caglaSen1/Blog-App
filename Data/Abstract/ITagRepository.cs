using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> GetAll { get; }

        void Add(Tag entity);
    }
}
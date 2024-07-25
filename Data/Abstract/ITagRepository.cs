using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();

        Task<Tag> GetByName(string name);

        Task<Tag> GetById(int id);

        Task<IEnumerable<Tag>> GetByIds(List<int> tagIds);

        void Add(Tag entity);
    }
}
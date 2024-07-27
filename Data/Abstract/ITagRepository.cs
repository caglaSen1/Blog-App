using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();

        Task<Tag> GetByName(string name);

        Task<Tag> GetById(int id);

        Task<Tag> GetByUrl(string url);

        Task<IEnumerable<Tag>> GetByIds(List<int> tagIds);

        Task<List<Tag>> GetPopularTags(int amount);

        void Add(Tag entity);

        void Delete(Tag tag);
    }
}
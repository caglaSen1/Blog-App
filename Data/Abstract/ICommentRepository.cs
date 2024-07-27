using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

    public interface ICommentRepository{

        Task<List<Comment>> GetAll();

        Task<Comment> GetById(int id);

        Task<Comment> GetByUrl(string url);
        
        void Create(Comment comment);

        void Delete(Comment comment);
    }
}
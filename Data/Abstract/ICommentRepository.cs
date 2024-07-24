using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

    public interface ICommentRepository{

        Task<List<Comment>> GetAll();

        Task<Comment> GetById(int id);
        
        void CreateComment(Comment comment);
    }
}
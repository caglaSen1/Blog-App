using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

    public interface IUserRepository{

        Task<List<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> GetByEmail(string email);

        void CreateUser(User user);
    }
}
using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

    public interface IUserRepository{

        Task<List<User>> GetAll();

        Task<User?> GetById(int id);

        Task<User> GetByUrl(string url);

        Task<User?> GetByEmail(string email);

        Task<User?> GetByUserName(string userName);

        Task<User?> GetByEmailAndPassword(string email, string password);

        Task<User?> GetByEmailAndUserName(string email, string userName);

        void Create(User user);

        void Delete(User user);
    }
}
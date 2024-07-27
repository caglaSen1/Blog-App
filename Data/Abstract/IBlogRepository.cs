using BlogApp.Entity;
using BlogApp.Models;
using SQLitePCL;

namespace BlogApp.Data.Abstract
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAll();

        Task<PagedResult<Blog>> GetPagedBlogs(int pageNumber, int pageSize, string tagUrl, string search, int? userId);

        Task<Blog> GetById(int id);
        
        Task<Blog> GetByUrl(string url);

        Task<List<Blog>> GetBlogsByUserId(int userId);

        Task<int> GetCommentCount(int blogId);

        Task<int> GetLikeCount(int blogId);

        Task<List<Blog>> GetPopularBlogs(int count);

        void Add(Blog blog);

        void Update(Blog blog);

        void Delete(Blog blog);

        void LikeBlog(int blogId);
    }
}
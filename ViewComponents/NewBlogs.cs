using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{

    public class NewBlogs : ViewComponent
    {

        private readonly IBlogRepository _blogRepository;

        public NewBlogs(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogRepository.GetAll();
            var newBlogs = blogs.OrderByDescending(b => b.CreatedAt).Take(5).ToList();
            return View(newBlogs);
        }
    }
}
using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{

    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            return View(
                new BlogViewModel
                {
                    Blogs = _blogRepository.GetAll().ToList()
                }
            );
        }

        public async Task<IActionResult> Details(string url)
        {
            var blog = await _blogRepository.GetByUrl(url);

            return View(blog);
        }

        public async Task<IActionResult> List(string tagUrl)
        {
            var blogs = _blogRepository.GetAll();

            if (!string.IsNullOrEmpty(tagUrl))
            {
                blogs = blogs.Where(b => b.Tags.Any(t => t.Url == tagUrl));
            }


            return View(new BlogViewModel
            {
                Blogs = await blogs.ToListAsync()
            });

        }
    }
}
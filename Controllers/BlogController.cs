using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

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
                    Blogs = _blogRepository.GetAll.ToList()
                }
            );
        }
    }
}
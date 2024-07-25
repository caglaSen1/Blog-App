using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{

    public class AdminController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITagRepository _tagRepository;

        public AdminController(IBlogRepository blogRepository, IUserRepository userRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Home()
        {

            var ManagementViewModel = new ManagementViewModel{
                Blogs = await _blogRepository.GetAll(),
                Tags = await _tagRepository.GetAll(),
                Users = await _userRepository.GetAll(),
                Comments = await _commentRepository.GetAll()
            };
                        
            return View(ManagementViewModel);
        }

        public async Task<IActionResult> Edit(string url)
        {
            var blog = await _blogRepository.GetByUrl(url);
            var tags = await _tagRepository.GetAll();

            var model = new BlogEditViewModel
            {
                BlogTitle = blog.Title,
                BlogContent = blog.Content,
                BlogDescription = blog.Description,
                BlogImage = blog.Image,
                Tags = tags,
                SelectedTags = blog.Tags.Select(t => t.Id).ToList()
            };

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string url, BlogEditViewModel model, IFormFile imageFile)
        {
            var blog = await _blogRepository.GetByUrl(url);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Title = model.BlogTitle;
            blog.Content = model.BlogContent;
            blog.Description = model.BlogDescription;

            if (imageFile != null)
            {
                var allowenExtensions = new[] { ".jpg", ".png", ".jpeg" };
                var extensions = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                if (!allowenExtensions.Contains(extensions))
                {
                    ModelState.AddModelError("", "Geçerli bir resim seçiniz!");
                }
                else
                {
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                    try
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        blog.Image = randomFileName;
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                    }
                }
            }

            var selectedTags = await _tagRepository.GetByIds(model.SelectedTags);
            blog.Tags.Clear();
            blog.Tags.AddRange(selectedTags);

            _blogRepository.Update(blog);

            return RedirectToAction("Home");
        }

        public async Task<IActionResult> Delete(string url)
        {

            var blog = await _blogRepository.GetByUrl(url);

            return View("DeleteConfirm", blog);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var blogToDelete = await _blogRepository.GetById(id);

            if (blogToDelete == null)
            {
                return NotFound();
            }

            _blogRepository.Delete(blogToDelete);
            return RedirectToAction("Home");
        }
        
    }
}
using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.Controllers
{

    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        private readonly ICommentRepository _commentRepository;

        private readonly ITagRepository _tagRepository;

        public BlogController(IBlogRepository blogRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _blogRepository = blogRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var tags = await _tagRepository.GetAll();
            var model = new BlogCreateViewModel
            {
                Tags = tags
            };
            return View(model);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogCreateViewModel model, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                //const int maxFileSize = 2 * 1024;
                var allowenExtensions = new[] { ".jpg", ".png", ".jpeg" };
                if (imageFile != null)
                {

                    /*if (imageFile.Length > maxFileSize)
                    {
                        ModelState.AddModelError("", "Dosya boyutu 2 MB den küçük olmalıdır.");
                    }*/

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

                            model.BlogImage = randomFileName;
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                        }
                    }
                }
                else
                {
                    model.BlogImage = "defaultBlog.png";
                }

                var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdValue, out int userId))
                {
                    return Json(new { error = "Geçersiz kullanıcı ID." });
                }

                var blog = new Blog(model.BlogTitle, model.BlogContent, model.BlogDescription, model.BlogImage, userId);

                var selectedTags = await _tagRepository.GetByIds(model.SelectedTags);
                blog.Tags.AddRange(selectedTags);

                _blogRepository.Add(blog);

                return RedirectToAction("List");
            }
            else
            {                
                model.Tags = await _tagRepository.GetAll();
                return View(model);
            }
        }

        public async Task<IActionResult> Details(string url)
        {
            var blog = await _blogRepository.GetByUrl(url);

            return View(blog);
        }

        public async Task<IActionResult> List(string tagUrl, string searchString)
        {
            var claims = User.Claims;
            var blogs = await _blogRepository.GetAll();
            var tags = await _tagRepository.GetAll();

            if (!string.IsNullOrEmpty(tagUrl))
            {
                blogs = blogs.Where(b => b.Tags.Any(t => t.Url == tagUrl)).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(b => b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                      || b.Content.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(new BlogViewModel
            {
                Blogs = blogs,
                Tags = tags
            });

        }

        [HttpPost]
        [Authorize]
        public JsonResult AddComment(int blogId, string text)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userImage = User.FindFirstValue(ClaimTypes.UserData);

            if (!int.TryParse(userIdValue, out int userId))
            {
                return Json(new { error = "Geçersiz kullanıcı ID." });
            }

            var comment = new Comment(text, blogId, userId);

            _commentRepository.CreateComment(comment);

            return Json(new
            {
                userName,
                text,
                commentTime = comment.CreatedAt,
                userImage
            });
        }

        [HttpPost]
        [Authorize]
        public JsonResult Like(int blogId)
        {
            try
            {
                _blogRepository.LikeBlog(blogId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [Authorize]
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

            return View(model);
        }

        [HttpPost]
        [Authorize]
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

            return RedirectToAction("Manage");
        }

        [Authorize]
        public async Task<IActionResult> Manage(string tagUrl, string searchString)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tags = await _tagRepository.GetAll();

            if (!int.TryParse(userIdValue, out int userId))
            {
                return Json(new { error = "Geçersiz kullanıcı ID." });
            }

            var blogs = await _blogRepository.GetBlogsByUserId(userId);

            if (!string.IsNullOrEmpty(tagUrl))
            {
                blogs = blogs.Where(b => b.Tags.Any(t => t.Url == tagUrl)).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(b => b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                      || b.Content.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View("Manage", new BlogViewModel
            {
                Blogs = blogs,
                Tags = tags
            });
        }

        [Authorize]
        public async Task<IActionResult> Delete(string url)
        {

            var blog = await _blogRepository.GetByUrl(url);

            return View("DeleteConfirm", blog);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var blogToDelete = await _blogRepository.GetById(id);

            if (blogToDelete == null)
            {
                return NotFound();
            }

            _blogRepository.Delete(blogToDelete);
            return RedirectToAction("Manage");
        }

    }
}
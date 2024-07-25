using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create()
        {
            var tags = await _tagRepository.GetAll();

            var model = new BlogCreateViewModel
            {
                Tags = tags.Select(tag => new TagViewModel { Id = tag.Id, Name = tag.Name, IsSelected = false }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateViewModel model, string selectedTags, IFormFile imageFile)
        {
            var selectedTagList = new List<int>();
            if (!string.IsNullOrEmpty(selectedTags))
            {
                selectedTagList = selectedTags.Split(',').Select(int.Parse).ToList();
            }

            const int maxFileSize = 2 * 1024;
            var allowenExtensions = new[] { ".jpg", ".png", ".jpeg" };
            if (imageFile != null)
            {

                if (imageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("", "Dosya boyutu 2 MB den küçük olmalıdır.");
                }

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

            var Blog = new Blog(model.BlogTitle, model.BlogContent, model.BlogDescription, model.BlogImage, userId);
            _blogRepository.Add(Blog);

            return RedirectToAction("List");

        }



        /*
                                [HttpPost]
                public async Task<IActionResult> Create(BlogCreateViewModel model, IFormFile imageFile)
                {
                    

                    //TODO:
                    if (!ModelState.IsValid)
                    {
                        if (model.Blog.Title != null)
                        {
                            model.Blog.Url = model.Blog.Title.ToLower().Replace(" ", "-");
                        }

                        model.Blog.CreatedAt = DateTime.Now;
                        model.Blog.IsActive = true;

                        // TODO:
                        model.Blog.UserId = 1;


                                        var selectedTags = model.Tags.Where(t => t.IsSelected).ToList();
                                        foreach (var tagViewModel in selectedTags)
                                        {
                                            var tag = await _tagRepository.GetById(tagViewModel.Id);
                                            if (tag != null)
                                            {
                                                model.Blog.Tags.Add(tag);
                                            }
                                        }


                        _blogRepository.Add(model.Blog);
                        return RedirectToAction("List");
                    }
                    else
                    {
                        foreach (var state in ModelState)
                        {
                            foreach (var error in state.Value.Errors)
                            {
                                Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                            }
                        }

                        var tags = await _tagRepository.GetAll();
                        if (tags == null)
                        {
                            model.Tags = new List<TagViewModel>(); // Or handle the null case appropriately
                        }
                        else
                        {
                            model.Tags = tags
                                .Select(tag => new TagViewModel
                                {
                                    Id = tag.Id,
                                    Name = tag.Name,
                                    IsSelected = model.Tags != null && model.Tags.Any(t => t.Id == tag.Id && t.IsSelected)
                                })
                                .ToList();
                        }
                    }

                    return View(model);
                }*/

        public async Task<IActionResult> Details(string url)
        {
            var blog = await _blogRepository.GetByUrl(url);

            return View(blog);
        }

        public async Task<IActionResult> List(string tagUrl)
        {
            var claims = User.Claims;
            var blogs = await _blogRepository.GetAll();

            if (!string.IsNullOrEmpty(tagUrl))
            {
                blogs = blogs.Where(b => b.Tags.Any(t => t.Url == tagUrl)).ToList();
            }


            return View(new BlogViewModel
            {
                Blogs = blogs
            });

        }

        [HttpPost]
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

        public async Task<IActionResult> ListByUser()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out int userId))
            {
                return Json(new { error = "Geçersiz kullanıcı ID." });
            }

            var blogs = await _blogRepository.GetBlogsByUserId(userId);

            return View("Manage", new BlogViewModel
            {
                Blogs = blogs
            });
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
            return RedirectToAction("ListByUser");
        }

    }
}
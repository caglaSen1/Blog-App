using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using BlogApp.Entity;
using BlogApp.Data.Abstract.BusinessRules;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.Controllers
{

    public class AdminController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagBusinessRules _tagBusinessRules;

        public AdminController(IBlogRepository blogRepository, IUserRepository userRepository, ICommentRepository commentRepository, ITagRepository tagRepository, ITagBusinessRules tagBusinessRules)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
            _tagBusinessRules = tagBusinessRules;
        }

        [Authorize]
        public async Task<IActionResult> Home()
        {

            var ManagementViewModel = new ManagementViewModel
            {
                Blogs = await _blogRepository.GetAll(),
                Tags = await _tagRepository.GetAll(),
                Users = await _userRepository.GetAll(),
                Comments = await _commentRepository.GetAll()
            };

            return View(ManagementViewModel);
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

            return View("Edit", model);
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

            TempData["SuccessMessage"] = "Blog başarıyla güncellendi.";
            return RedirectToAction("Home");
        }


        [Authorize]
        public async Task<IActionResult> CreateTag()
        {
            var model = new TagCreateViewModel
            {
                Colors = Enum.GetValues(typeof(TagColors))
                            .Cast<TagColors>()
                            .Select(tc => tc.ToString())
                            .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTag(TagCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_tagBusinessRules.AnyTagExistWithName(model.Name) == false)
                {
                    var tagColor = Enum.Parse<TagColors>(model.SelectedColorStr);
                    var tag = new Tag(model.Name, tagColor);

                    _tagRepository.Add(tag);

                    TempData["SuccessMessage"] = "Etiket başarıyla oluşturuldu.";
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", "Bu isimde bir etiket zaten mevcut.");
                }

            }

            model.Colors = Enum.GetValues(typeof(TagColors))
                       .Cast<TagColors>()
                       .Select(tc => tc.ToString())
                       .ToList();


            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> DeleteBlog(string url)
        {

            var blog = await _blogRepository.GetByUrl(url);

            var model = new DeleteViewModel
            {
                EntityId = blog.Id,
                EntityName = blog.Title,
                ActionUrl = "/admin/deleteBlog"
            };

            return View("DeleteConfirm", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteBlog(int entityId)
        {
            var blogToDelete = await _blogRepository.GetById(entityId);

            if (blogToDelete == null)
            {
                return NotFound();
            }

            _blogRepository.Delete(blogToDelete);

            TempData["SuccessMessage"] = "Blog başarıyla silindi.";
            return RedirectToAction("Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteUser(string url)
        {

            var user = await _userRepository.GetByUrl(url);

            var model = new DeleteViewModel
            {
                EntityId = user.Id,
                EntityName = user.UserName,
                ActionUrl = "/admin/deleteUser"
            };

            return View("DeleteConfirm", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int entityId)
        {
            var userToDelete = await _userRepository.GetById(entityId);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _userRepository.Delete(userToDelete);

            TempData["SuccessMessage"] = "Kullanıcı başarıyla silindi.";
            return RedirectToAction("Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteTag(string url)
        {

            var tag = await _tagRepository.GetByUrl(url);

            var model = new DeleteViewModel
            {
                EntityId = tag.Id,
                EntityName = tag.Name,
                ActionUrl = "/admin/deleteTag"
            };

            return View("DeleteConfirm", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteTag(int entityId)
        {
            var tagToDelete = await _tagRepository.GetById(entityId);

            if (tagToDelete == null)
            {
                return NotFound();
            }

            _tagRepository.Delete(tagToDelete);

            TempData["SuccessMessage"] = "Etiket başarıyla silindi.";
            return RedirectToAction("Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteComment(string url)
        {

            var comment = await _commentRepository.GetByUrl(url);

            var model = new DeleteViewModel
            {
                EntityId = comment.Id,
                EntityName = comment.Text.Substring(0, 10),
                ActionUrl = "/admin/deleteComment"
            };

            return View("DeleteConfirm", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int entityId)
        {
            var commentToDelete = await _commentRepository.GetById(entityId);

            if (commentToDelete == null)
            {
                return NotFound();
            }

            _commentRepository.Delete(commentToDelete);

            TempData["SuccessMessage"] = "Yorum başarıyla silindi.";
            return RedirectToAction("Home");
        }

    }
}
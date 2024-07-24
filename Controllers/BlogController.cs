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

/*
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
        public async Task<IActionResult> Create(BlogCreateViewModel model, IFormFile imageFile)
        {
            //const int maxFileSize = 4 * 1024; // 2MB
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (imageFile != null)
            {
                /*
                if (imageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("imageFile", $"Dosya boyutu {maxFileSize / 1024} MB'dan küçük olmalıdır.");
                }

                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("imageFile", "Geçerli bir resim dosyası seçiniz.");
                }
                else
                {
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");  // Guid: Global Unique Identifier. Benzersiz bir id (image adı) oluşturur

                    // dosyanın kaydedileceği yolu belirler
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                    // dosyayı kaydetmek aslında onu kopyalamaktır. Bu işlemin hatasız gerçekleştiğini bilmemiz gerekir
                    try
                    {
                        using (var stream = new FileStream(path, FileMode.Create)) // path yoluna dosyayı oluşturur, file oluşturma işlemi
                        {
                            await imageFile.CopyToAsync(stream);  // resmi oluşturduğumuz yolun içinde oluşturduğumuz dosyaya kopyalar. 
                        }

                        // product model içindeki image değerine randomFileName'i atar
                        model.Blog.Image = randomFileName;
                    }
                    catch
                    {
                        ModelState.AddModelError("imageFile", "Dosya yüklenirken bir hata oluştu.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("imageFile", "Bir resim dosyası seçiniz.");
            }


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
        public JsonResult AddComment(int BlogId,string Text){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);
            
            var comment = new Comment(Text, BlogId, int.Parse(userId ?? ""));

            _commentRepository.CreateComment(comment);
            
            return Json(new{
                username,
                Text,
                comment.CreatedAt,
                avatar
            });
        }

/*
        public IActionResult Delete(string url)
    {
        if (url == null)
        {
            return NotFound();
        }

        var blog = _blogRepository.GetByUrl(url);

        if (blog == null)
        {
            return NotFound();
        }

        return View("DeleteConfirm", blog);
    }

    [HttpPost]
    public IActionResult Delete(int id, int ProductId)
    {
        if (id != ProductId)
        {
            return NotFound();
        }

        var bootcamp = Repository.GetProductById(id);

        if (bootcamp == null)
        {
            return NotFound();
        }

        Repository.DeleteProduct(bootcamp);
        return RedirectToAction("Index");
    }*/
    }
}
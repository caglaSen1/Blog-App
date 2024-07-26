using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("List", "Blog");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByEmailAndPassword(model.Email, model.Password);

                if (user != null)
                {
                    var userClaims = new List<Claim>
                    {
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new(ClaimTypes.Name, user.UserName),
                        new(ClaimTypes.GivenName, user.FirstName),
                        new(ClaimTypes.GivenName, user.LastName),
                        new(ClaimTypes.UserData, user.Image)
                    };

                    if (user.Email == "admin@gmail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var autProperties = new AuthenticationProperties { IsPersistent = true };
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), autProperties
                    );

                    return RedirectToAction("List", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, IFormFile? imageFile)
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
                            model.Image = randomFileName;
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                        }
                    }
                }
                else
                {
                    model.Image = "defaultUser.png";
                }

                if (await _userRepository.GetByUserName(model.UserName) != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı adı kullanımda.");
                    return View(model);
                }
                else if (await _userRepository.GetByEmail(model.Email) != null)
                {
                    ModelState.AddModelError("", "Bu email adresi kullanımda.");
                    return View(model);
                }
                else
                {
                    _userRepository.CreateUser(new User(model.UserName, model.FirstName, model.LastName, model.Email, model.Password, model.Image));

                    return RedirectToAction("Login");
                }
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Profile(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return NotFound();
            }
            var user = await _userRepository.GetByUserName(userName);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
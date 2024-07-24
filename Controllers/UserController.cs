using System.Security.Claims;
using BlogApp.Data.Abstract;
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

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("List", "Blog");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
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
                        new(ClaimTypes.UserData, user.Image ?? "")
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
    }
}
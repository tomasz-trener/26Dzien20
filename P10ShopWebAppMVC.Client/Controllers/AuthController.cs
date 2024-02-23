using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace P10ShopWebAppMVC.Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var response = await _authService.Login(model);

            if (response.Success)
            {
                HttpContext.Session.SetString("JWTToken", response.Data);

                // Tworzenie identyfikatora użytkownika
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    // Możesz dodać więcej claimów w zależności od otrzymanych danych
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("JWTToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }

}

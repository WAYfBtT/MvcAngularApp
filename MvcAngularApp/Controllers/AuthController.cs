using BLL.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BllInterfaces = BLL.Interfaces;

namespace MvcAngularApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly BllInterfaces.IAuthenticationService _authenticationService;

        public AuthController(BllInterfaces.IAuthenticationService authenticationService) => _authenticationService = authenticationService;


        [HttpGet("SignIn")]
        public IActionResult SignIn()
        {
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var claims = await _authenticationService.SignInAsync(model);
            if (claims.Claims.Any())
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "Invalid Username or password.");
            return View(model);

        }

        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _authenticationService.SignUp(model))
                return RedirectToAction("SignIn", "Auth");
            ModelState.AddModelError("Username", "Username already taken.");

            return View(model);
        }

        [HttpGet("SignOut")]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Auth");
        }
    }
}

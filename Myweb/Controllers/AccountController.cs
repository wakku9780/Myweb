using Microsoft.AspNetCore.Mvc;
using Myweb.Models;
using Myweb.Services;
using Myweb.ViewModels;

namespace Myweb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    // Hash the password before storing it in the database in a production environment
                };

                var result = await _userService.RegisterUserAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Redirect to login page after successful registration
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If registration fails, return to register page with validation errors
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model.Username, model.Password);

                if (result.Succeeded)
                {
                    // Redirect to home page after successful login
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If login fails, return to login page with error message
            return View(model);
        }
    }
}

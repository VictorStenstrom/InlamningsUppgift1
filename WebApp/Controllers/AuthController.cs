using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            var viewModel = new SignInViewModel();
            if (returnUrl == null)
            {
                viewModel.ReturnUrl = "/";
            }
            else
            {
                viewModel.ReturnUrl = returnUrl;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, false);
                if (result.Succeeded)
                    if (model.ReturnUrl == null || model.ReturnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
            }

            ModelState.AddModelError("", "Incorrect email address or password");

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
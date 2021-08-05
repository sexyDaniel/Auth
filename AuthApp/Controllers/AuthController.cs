using AuthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IIdentityServerInteractionService interactionService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IIdentityServerInteractionService interactionService) =>
            (this.userManager,this.signInManager,this.interactionService) = (userManager,signInManager, interactionService);

        [HttpGet]
        public IActionResult Login(string returnUrl) 
        {
            var view = new LoginViewModel { ReturnUrl = returnUrl };
            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(viewModel);
            }
            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user == null) 
            {
                ModelState.AddModelError(string.Empty,"User not found");
                return View(viewModel);
            }
            var result =await signInManager.PasswordSignInAsync(viewModel.Email,viewModel.Password,false,false);

            if (result.Succeeded) 
            {
                Redirect(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Login Error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var view = new RegisterViewModel { ReturnUrl = returnUrl };
            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)  
            {
                return View(viewModel);
            }
            var user = new User()
            {
                UserName = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };

            var result = await userManager.CreateAsync(user,viewModel.Password);
            if (result.Succeeded) 
            {
                await signInManager.SignInAsync(user, false);
                return Redirect("https://vk.com/audios220850711");
            }
            return View(viewModel);
        }
    }
}

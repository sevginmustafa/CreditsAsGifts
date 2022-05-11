using CreditsAsGifts.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditsAsGifts.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return this.View();
        }
    }
}

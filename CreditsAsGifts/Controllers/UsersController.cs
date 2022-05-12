using CreditsAsGifts.Data.Models;
using CreditsAsGifts.Infrastructure.Extensions;
using CreditsAsGifts.Services.Users;
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
        private readonly IUsersService usersService;

        public UsersController(SignInManager<ApplicationUser> signInManager, IUsersService usersService)
        {
            this.signInManager = signInManager;
            this.usersService = usersService;
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

        [Authorize]
        public async Task<IActionResult> MyTransactionsAsync()
        {
            this.ViewData["UserPhoneNumber"] = await this.usersService
                .GetUserPhoneNumberAsync(this.User.GetId());

            return this.View(await this.usersService.GetUserTransactionsAsync(this.User.GetId()));
        }
    }
}

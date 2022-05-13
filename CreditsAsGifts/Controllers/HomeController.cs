using CreditsAsGifts.Data.Models;
using CreditsAsGifts.Models;
using CreditsAsGifts.Services.Privacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CreditsAsGifts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPrivacyService privacyService;

        public HomeController(IPrivacyService privacyService)
        {
            this.privacyService = privacyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PrivacyAsync()
        {
            return View(await this.privacyService.GetPrivacyAsync());
        }

        public async Task<IActionResult> TermsAndConditionsAsync()
        {
            return View(await this.privacyService.GetTermsAndConditionsAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            return this.View("~/Views/Shared/404.cshtml");
        }
    }
}

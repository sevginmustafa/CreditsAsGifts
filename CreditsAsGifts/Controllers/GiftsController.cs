namespace CreditsAsGifts.Controllers
{
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Services.Gifts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    using static CreditsAsGifts.Infrastructure.Extensions.ClaimsPrincipalExtensions;

    public class GiftsController : Controller
    {
        private readonly IGiftsService giftsService;

        public GiftsController(IGiftsService giftsService)
        {
            this.giftsService = giftsService;
        }

        [Authorize]
        public IActionResult SendGift()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendGiftAsync(GiftSendInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.giftsService.SendGiftAsync(this.User.GetId(), inputModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction(nameof(UnsuccessMessage));
            }

            return this.RedirectToAction(nameof(SuccessMessage));
        }

        public IActionResult SuccessMessage()
        {
            return this.View();
        }

        public IActionResult UnsuccessMessage()
        {
            return this.View();
        }
    }
}

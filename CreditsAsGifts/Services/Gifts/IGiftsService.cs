﻿namespace CreditsAsGifts.Services.Gifts
{
    using CreditsAsGifts.Models.Gifts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGiftsService
    {
        public Task<int> GetUserCurrentCreditsAsync(string userId);

        public Task SendGiftAsync(string senderId, GiftSendInputModel inputModel);
    }
}

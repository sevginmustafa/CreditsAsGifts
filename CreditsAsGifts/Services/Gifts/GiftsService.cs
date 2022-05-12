using AutoMapper;
using CreditsAsGifts.Data;
using CreditsAsGifts.Data.Models;
using CreditsAsGifts.Models.Gifts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditsAsGifts.Services.Gifts
{
    public class GiftsService : IGiftsService
    {
        private readonly CreditsAsGiftsDbContext database;
        private readonly IMapper mapper;

        public GiftsService(CreditsAsGiftsDbContext database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public async Task<int> GetUserCurrentCreditsAsync(string userId)
        {
            var user = await this.database.ApplicationUsers.FindAsync(userId);

            return user.Credits;
        }

        public async Task SendGiftAsync(string senderId, GiftSendInputModel inputModel)
        {
            var sender = await this.database.ApplicationUsers.FindAsync(senderId);

            var recipient = await this.database.ApplicationUsers
                .FirstOrDefaultAsync(x => x.PhoneNumber == inputModel.PhoneNumber);

            if (recipient == null)
            {
                throw new ArgumentNullException();
            }

            if (sender.Credits < inputModel.NumberOfCredits
                || sender.PhoneNumber == inputModel.PhoneNumber)
            {
                throw new ArgumentException();
            }

            var gift = new Gift
            {
                Date = DateTime.UtcNow,
                From = sender.PhoneNumber,
                To = inputModel.PhoneNumber,
                NumberOfCredits = inputModel.NumberOfCredits,
                Message = inputModel.Message
            };

            sender.Credits -= inputModel.NumberOfCredits;
            recipient.Credits += inputModel.NumberOfCredits;

            await this.database.Gifts.AddAsync(gift);
            await this.database.SaveChangesAsync();
        }
    }
}

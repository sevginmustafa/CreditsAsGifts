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

            var giftSended = new GiftSended
            {
                Recipient = recipient,
                NumberOfCredits = inputModel.NumberOfCredits,
                Comment = inputModel.Comment,
                Date = DateTime.UtcNow
            };

            var giftReceived = new GiftReceived
            {
                Sender = sender,
                NumberOfCredits = inputModel.NumberOfCredits,
                Comment = inputModel.Comment,
                Date = DateTime.UtcNow
            };

            sender.Credits -= inputModel.NumberOfCredits;
            recipient.Credits += inputModel.NumberOfCredits;

            await this.database.GiftsSended.AddAsync(giftSended);
            await this.database.GiftsReceived.AddAsync(giftReceived);

            await this.database.SaveChangesAsync();
        }
    }
}

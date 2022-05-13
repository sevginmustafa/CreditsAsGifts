namespace CreditsAsGifts.Services.Users
{
    using AutoMapper;
    using CreditsAsGifts.Data;
    using CreditsAsGifts.Models.Gifts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly CreditsAsGiftsDbContext database;
        private readonly IMapper mapper;

        public UsersService(CreditsAsGiftsDbContext database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public bool IsEmailAvailable(string email)
            => !this.database.ApplicationUsers.Any(x => x.Email.ToLower() == email.ToLower());

        public bool IsPhoneNumberAvailable(string phoneNumber)
            => !this.database.ApplicationUsers.Any(x => x.PhoneNumber == phoneNumber);

        public async Task<IEnumerable<TransactionViewModel>> GetUserTransactionsAsync(string userId)
        {
            var user = await this.database.ApplicationUsers.FindAsync(userId);

            var gifts = this.database.Gifts
                .Where(x => x.From == user.PhoneNumber || x.To == user.PhoneNumber);

            var transactions = new List<TransactionViewModel>();

            foreach (var gift in gifts)
            {
                var sender = await this.database.ApplicationUsers.FirstOrDefaultAsync(x => x.PhoneNumber == gift.From);
                var recipient = await this.database.ApplicationUsers.FirstOrDefaultAsync(x => x.PhoneNumber == gift.To);

                transactions.Add(new TransactionViewModel
                {
                    Date = gift.Date,
                    SenderUserName = sender.UserName,
                    SenderPhoneNumber = sender.PhoneNumber,
                    RecipientUserName = recipient.UserName,
                    RecipientPhoneNumber = recipient.PhoneNumber,
                    NumberOfCredits = gift.NumberOfCredits,
                    Message = gift.Message
                });
            }

            return transactions.OrderByDescending(x => x.Date);
        }

        public async Task<string> GetUserPhoneNumberAsync(string userId)
        {
            var user = await this.database.ApplicationUsers.FindAsync(userId);

            return user.PhoneNumber;
        }

        public async Task<IEnumerable<TransactionViewModel>> SearchTransactionsByPhoneNumberAsync(string searchString, string userId)
        {
            var allTransactions = await GetUserTransactionsAsync(userId);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return allTransactions
                 .Where(x => x.SenderPhoneNumber.Contains(searchString) || x.RecipientPhoneNumber.Contains(searchString));
            }

            return allTransactions;
        }
    }
}

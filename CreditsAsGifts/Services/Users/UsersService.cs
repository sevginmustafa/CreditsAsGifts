namespace CreditsAsGifts.Services.Users
{
    using AutoMapper;
    using CreditsAsGifts.Data;
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Models.Users;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static CreditsAsGifts.Common.GlobalConstants;

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

        public async Task<IEnumerable<TransactionViewModel>> GetTransactionsAsync(string userId)
        {
            var gifts = this.database.Gifts.ToList();

            if (userId != null)
            {
                var user = await this.database.ApplicationUsers.FindAsync(userId);

                gifts = gifts.Where(x => x.From == user.PhoneNumber || x.To == user.PhoneNumber).ToList();
            }

            var transactions = new List<TransactionViewModel>();

            foreach (var gift in gifts)
            {
                var sender = await this.database.ApplicationUsers.FirstOrDefaultAsync(x => x.PhoneNumber == gift.From);
                var recipient = await this.database.ApplicationUsers.FirstOrDefaultAsync(x => x.PhoneNumber == gift.To);

                transactions.Add(new TransactionViewModel
                {
                    Date = gift.Date,
                    SenderName = sender.FirstName + " " + sender.LastName,
                    SenderPhoneNumber = sender.PhoneNumber,
                    RecipientName = recipient.FirstName + " " + recipient.LastName,
                    RecipientPhoneNumber = recipient.PhoneNumber,
                    NumberOfCredits = gift.NumberOfCredits,
                    Message = gift.Message
                });
            }

            return transactions.OrderByDescending(x => x.Date);
        }

        public async Task<IEnumerable<TransactionViewModel>> SearchTransactionsByPhoneNumberAsync(string searchString, string userId)
        {
            var allTransactions = await GetTransactionsAsync(userId);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return allTransactions
                 .Where(x => x.SenderPhoneNumber.Contains(searchString) || x.RecipientPhoneNumber.Contains(searchString));
            }

            return allTransactions;
        }

        public async Task<string> GetUserPhoneNumberAsync(string userId)
        {
            var user = await this.database.ApplicationUsers.FindAsync(userId);

            return user.PhoneNumber;
        }

        public IEnumerable<UserAdministrationViewModel> GetAllUsers()
            => this.mapper
            .Map<IEnumerable<UserAdministrationViewModel>>(this.database.ApplicationUsers
                .Where(x => x.PhoneNumber != AdministratorPhoneNumber));

        public IEnumerable<UserAdministrationViewModel> SearchUsersByPhoneNumber(string searchString)
        {
            var allUsers = GetAllUsers();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return allUsers
                 .Where(x => x.PhoneNumber.Contains(searchString));
            }

            return allUsers.OrderBy(x => x.UserName);
        }
    }
}

namespace CreditsAsGifts.Services.Users
{
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Models.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        public bool IsEmailAvailable(string email);

        public bool IsPhoneNumberAvailable(string phoneNumber);

        public IEnumerable<UserAdministrationViewModel> GetAllUsers();

        public Task<string> GetUserPhoneNumberAsync(string userId);

        public Task<IEnumerable<TransactionViewModel>> GetTransactionsAsync(string userId = null);

        public Task<IEnumerable<TransactionViewModel>> GetIncomingTransactionsAsync(string userId);

        public Task<IEnumerable<TransactionViewModel>> GetOutgoingTransactionsAsync(string userId);

        public Task<IEnumerable<TransactionViewModel>> SearchTransactionsByPhoneNumberAsync(
            string searchString, int transactionType = 0, string userId = null);

        public IEnumerable<UserAdministrationViewModel> SearchUsersByPhoneNumber(string searchString);
    }
}

namespace CreditsAsGifts.Services.Users
{
    using CreditsAsGifts.Models.Gifts;
    using CreditsAsGifts.Models.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        public bool IsEmailAvailable(string email);

        public bool IsPhoneNumberAvailable(string phoneNumber);

        public IEnumerable<UserAdministrationViewModel> GetAllUsers();

        public Task<string> GetUserPhoneNumberAsync(string userId);

        public Task<IEnumerable<TransactionViewModel>> GetUserTransactionsAsync(string userId);

        public Task<IEnumerable<TransactionViewModel>> SearchTransactionsByPhoneNumberAsync(string searchString, string userId);

        public IEnumerable<UserAdministrationViewModel> SearchUsersByPhoneNumber(string searchString);
    }
}

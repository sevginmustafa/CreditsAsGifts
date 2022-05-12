namespace CreditsAsGifts.Services.Users
{
    using CreditsAsGifts.Models.Gifts;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        public bool IsEmailAvailable(string email);

        public bool IsPhoneNumberAvailable(string phoneNumber);

        public Task<IEnumerable<TransactionViewModel>> GetUserTransactionsAsync(string userId);
    }
}
